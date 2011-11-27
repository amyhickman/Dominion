using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using Dominion.Util;
using log4net;
using Dominion.PendingEventModel;
using System.Diagnostics;
using Dominion.Interfaces;

namespace Dominion.Model
{
    public class Game
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Game));
        private static readonly CardCode[] _defaultSupplyCodes = new CardCode[] 
        {
            CardCode.Duchy, CardCode.Province, CardCode.Estate,
            CardCode.Gold, CardCode.Silver, CardCode.Copper
        };

        private readonly Dictionary<int, CardContainer> _containers = new Dictionary<int, CardContainer>();
        private readonly Dictionary<int, Card> _cardsById = new Dictionary<int, Card>();
        private readonly Dictionary<CardCode, CardContainer> _supplies = new Dictionary<CardCode, CardContainer>();
        private readonly List<Player> _players = new List<Player>();

        private readonly Queue<Turn> _turns = new Queue<Turn>();
        private readonly CardContainer _cardsInPlay = new CardContainer();
        private readonly CardContainer _cardsPlayed = new CardContainer();
        private readonly List<PendingAction> _pendingActions = new List<PendingAction>();
        private bool _started = false;

        private readonly CardFactory _cardFactory;

        private IGameObserver _observer = new DummyObserver();
        public IGameObserver Observer
        {
            get { return _observer; }
            set
            {
                if (value == null)
                    _observer = new DummyObserver();
            }
        }
        
        #region Constructors
        public Game()
        {
            _cardFactory = new CardFactory(this);
           
            RegisterContainer(_cardsInPlay);
            RegisterContainer(_cardsPlayed);
        }
        #endregion

        #region Game setup
        public void StartGame()
        {
            if (_supplies.Count < 10)
                throw new InvalidOperationException("Insufficient number of supplies.");

            if (_supplies.Count > 10)
                throw new InvalidOperationException("Too many supplies");

            if (_players.Count < 2)
                throw new InvalidOperationException("Not enough players.");

            foreach (CardCode code in _defaultSupplyCodes)
                CreateSupplyPile(code);

            _started = true;

            if (_cardsById.Any(kv => kv.Value.RequiresCurseSupply))
                CreateSupplyPile(CardCode.Curse);

            SetupTurnOrder(_players);

            foreach (var p in _players)
            {
                p.Deck.AddRange(_cardFactory.CreateCards(CardCode.Copper, 7));
                p.Deck.AddRange(_cardFactory.CreateCards(CardCode.Estate, 3));
                RegisterCards(p.Deck);
                p.Deck.Shuffle();
                DrawCards(p, 5);
            }

#if DEBUG
            foreach (var kv in _cardsById)
            {
                Card c = kv.Value;
                if (c.Container == null)
                    Debugger.Break();
            }
#endif

            

            NextTurn();
        }
        #endregion

        #region Turn management
        private void SetupRandomTurnOrder()
        {
            var x = _players
                .Select(p => new 
                {
                    player = p,
                    order = RNG.NextDouble()
                })
                .OrderBy(o => o.order)
                .Select(p => p.player);

            SetupTurnOrder(x);
        }

        private void SetupTurnOrder(IEnumerable<Player> players)
        {
            _turns.Clear();
            foreach (var p in players)
            {
                Turn t = new Turn(p);
                _turns.Enqueue(t);
            }
        }

        public void NextTurn()
        {
            _cardsInPlay.Clear();
            _cardsPlayed.Clear();

            CurrentTurn = _turns.Dequeue();
            if (CurrentTurn.IsRepeatable)
                _turns.Enqueue(new Turn(CurrentTurn.Owner));

            foreach (var effect in CurrentTurn.PendingEffects)
                effect.Apply(CurrentTurn);

            CurrentTurn.PendingEffects.Clear();

            if (CurrentTurn.IsPossessed)
            {
                Observer.OnPossessedTurnStart(CurrentTurn.Actor, CurrentTurn.Owner);
            }
            else
            {
                Observer.OnTurnStart(CurrentPlayer);
            }
        }

        public void EndTurn()
        {
            Player turnOwner = CurrentTurn.Owner;
            turnOwner.DiscardPile.AddRange(_cardsPlayed);

            Observer.OnTurnEnd(CurrentPlayer);
        }

        public Turn CurrentTurn { get; private set; }
        #endregion
        
        #region Container management
        private void RegisterContainer(CardContainer c)
        {
            if (c == null)
                throw new ArgumentNullException("c");

            if (!_containers.ContainsKey(c.ContainerId))
                _containers.Add(c.ContainerId, c);
        }
        #endregion

        #region Card management
        private void RegisterCard(Card c)
        {
            _log.Debug(String.Format("Registering card: {0}/{1}", c.Code, c.Id));
            if (!_cardsById.ContainsKey(c.Id))
                _cardsById.Add(c.Id, c);
        }

        private void RegisterCards(IEnumerable<Card> cards)
        {
            foreach (var c in cards)
                RegisterCard(c);
        }

        public void MoveCard(Card c, CardContainer targetContainer)
        {
            c.Container.Remove(c);
            targetContainer.Add(c);
            c.Container = targetContainer;
        }
        #endregion

        #region Supply management
        public void AddSupply(CardCode code)
        {
            if (!CardFactory.GetCardMeta(code).CanBeSupply)
                throw new InvalidOperationException("Card " + code.ToString() + " cannot be used as a supply.");

            if (_supplies.ContainsKey(code))
                throw new InvalidOperationException("Supply already added.");

            

            CreateSupplyPile(code);
        }

        private void CreateSupplyPile(CardCode code)
        {
            CardContainer pile = new CardContainer();
            int quantity = 10;

            switch (code)
            {
                case CardCode.Estate:
                case CardCode.Duchy:
                case CardCode.Province:
                    quantity = (Players.Count == 2) ? 8 : 12;
                    break;
            }

            _log.Info(String.Format("Generating supply pile for card: {0}, Quantity: {1}", code, quantity));

            for (int i = 0; i < quantity; i++)
            {
                Card card = _cardFactory.CreateCard(code);
                RegisterCard(card);
                pile.Add(card);
            }

            RegisterContainer(pile);
            _supplies.Add(code, pile);
        }
        #endregion

        #region Player management
        public ReadOnlyCollection<Player> Players { get { return new ReadOnlyCollection<Player>(_players); } }

        public void AddPlayer(Player p)
        {
            if (_started)
                throw new InvalidOperationException("Cannot add players after the game has started");

            if (p == null)
                throw new ArgumentNullException("p");

            _log.Info(String.Format("Adding player {0}:{1} to game", p.Principal.Identity.Name, p.Id));
            _players.Add(p);
            RegisterContainer(p.Deck);
            RegisterContainer(p.DiscardPile);
            RegisterContainer(p.Hand);
        }

        public Player CurrentPlayer { get { return CurrentTurn.Actor; } }

        public Player GetPlayerToLeft()
        {
            return _turns.ToList()[1].Owner;
        }

        public void ForEachOtherPlayer(Action<Player> action)
        {
            foreach (var p in Players)
            {
                if (p.Equals(CurrentPlayer))
                    continue;
                action(p);
            }
        }
        #endregion

        #region Game event management
        public void AddPendingAction(PendingAction action)
        {
            if (action == null) throw new ArgumentNullException("action");
            _pendingActions.Add(action);
        }

        public IList<PendingAction> PendingActions { get { return _pendingActions; } }
        #endregion

        #region Game activity
        public void RevealCard(Card card)
        {
            Observer.OnRevealCard(CurrentPlayer, card);
        }

        public void RevealCard(Player target, Card card)
        {
            Observer.OnRevealCard(target, card);
        }


        public void GainAction(int count = 1)
        {
            Observer.OnActionGain(count);
            CurrentTurn.ActionsRemaining += count;
        }

        public void GainTreasure(int count = 1)
        {
            Observer.OnTreasureGain(count);  
            CurrentTurn.TreasureRemaining += count;
        }

        public List<Card> DrawCards(int count)
        {
            return DrawCards(CurrentPlayer, count);
        }

        public List<Card> DrawCards(Player target, int count)
        {
            List<Card> retval = new List<Card>();
            for (int i = 0; i < count && target.Deck.Count > 0; i++)
            {
                Card c = target.Deck.Draw();
                retval.Add(c);
            }

            Observer.OnDrawCards(target, retval);
            return retval;
        }

        public Card GainCard(CardCode code)
        {
            return GainCard(CurrentPlayer, code);           
        }

        public Card GainCard(Player target, CardCode code)
        {
            if (!_supplies.ContainsKey(code)) throw new ArgumentOutOfRangeException("Supply piles do not include " + code.ToString());
            Card retval = null;

            CardContainer pile = _supplies[code];
            if (pile.Count > 0)
            {
                retval = pile.Draw();
            }

            Observer.OnGainCard(target, code);
            return retval;
        }

        public void GainBuy(int count = 1)
        {
            Observer.OnBuyGain(count);
            CurrentTurn.BuysRemaining += count;
        }

        public void PlayCard(int cardId)
        {
            Card c = _cardsById[cardId];

            //
            // Make sure the action can be played.  i.e. it needs to be in the players hand
            //
            if (c.Container.Equals(CurrentTurn.Owner.Hand))
            {
                Observer.OnCardPlayed(c);
                c.OnPlay();
            }
        }

        public void PutCardOnDeck(Card card)
        {
            Observer.OnPutCardOnDeck(CurrentPlayer, card);
            CurrentTurn.Owner.Deck.AddToTop(card);
        }

        public void RevealHand(Player target)
        {
            Observer.OnRevealHand(target, target.Hand.ToList());
        }

        public void ShuffleDeck(Player target)
        {
            target.Deck.Shuffle();
            Observer.OnShuffleDeck(target);
        }
        #endregion


    }
}