using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using Dominion.Util;
using Dominion.GameEventModel;
using log4net;

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

        private Dictionary<int, CardContainer> _containers = new Dictionary<int, CardContainer>();
        private Dictionary<int, Card> _cards = new Dictionary<int, Card>();
        private Dictionary<CardCode, CardContainer> _supplies = new Dictionary<CardCode, CardContainer>();
        private List<Player> _players = new List<Player>();

        private Queue<Turn> _turns = new Queue<Turn>();
        private Turn _currentTurn { get; set; }

        private CardContainer _cardsInPlay = new CardContainer();
        private CardContainer _cardsPlayed = new CardContainer();
        private List<PendingAction> _pendingActions = new List<PendingAction>();

        private CardFactory _cardFactory;
        private Random _rand = new Random();

        public event Action<GameEvent> OnPlayEvent;

        #region Constructors
        public Game(IEnumerable<Player> players, IList<CardCode> cardCodes)
        {
            _cardFactory = new CardFactory(this);

            foreach (var p in players)
            {
                if (p == null) throw new ArgumentOutOfRangeException("Cannot pass in null player");
                _log.Info(String.Format("Adding player {0}:{1} to game", p.Principal.Identity.Name, p.Id));
                AddPlayer(p);
            }

            foreach (CardCode code in _defaultSupplyCodes)
            {
                CreateSupplyPile(code);
            }
            
            GenerateSupplies(cardCodes);
            _currentTurn = null;

            RegisterContainer(_cardsInPlay);
            RegisterContainer(_cardsPlayed);

            SetupTurnOrder(_players);
        }
        #endregion

        #region Turn management
        private void SetupRandomTurnOrder()
        {
            var x = _players
                .Select(p => new 
                {
                    player = p,
                    order = _rand.NextDouble()
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

            _currentTurn = _turns.Dequeue();
            if (_currentTurn.IsRepeatable)
                _turns.Enqueue(new Turn(_currentTurn.Owner));

            foreach (var effect in _currentTurn.PendingEffects)
                effect.Apply(_currentTurn);

            _currentTurn.PendingEffects.Clear();
        }

        public void EndTurn()
        {
            Player turnOwner = _currentTurn.Owner;
            while (_cardsPlayed.Count > 0)
            {
                MoveCard(_cardsPlayed[0], turnOwner.DiscardPile);
            }
        }

        public Turn CurrentTurn { get { return _currentTurn; } }
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
            if (!_cards.ContainsKey(c.Id))
                _cards.Add(c.Id, c);
        }

        public void MoveCard(Card c, CardContainer targetContainer)
        {
            c.Container.Remove(c);
            targetContainer.Add(c);
            c.Container = targetContainer;
        }
        #endregion

        #region Supply management
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

        private void GenerateSupplies(IList<CardCode> codes)
        {
            if (codes.Count != 10)
                throw new ArgumentOutOfRangeException("10 codes are required.");

            _log.Info("Generating supplies.");
            
            foreach (CardCode c in codes)
            {
                CreateSupplyPile(c);
            }

            if (_cards.Any(kv => kv.Value.RequiresCurseSupply))
                CreateSupplyPile(CardCode.Curse);
        }
        #endregion

        #region Player management
        public ReadOnlyCollection<Player> Players { get { return new ReadOnlyCollection<Player>(_players); } }

        private void AddPlayer(Player p)
        {
            if (p == null)
                throw new ArgumentNullException("p");
            
            _players.Add(p);
            RegisterContainer(p.Deck);
            RegisterContainer(p.DiscardPile);
            RegisterContainer(p.Hand);
        }

        public Player CurrentPlayer { get { return _currentTurn.Actor; } }
        
        #endregion

        #region Game event management
        public void AddPendingAction(PendingAction action)
        {
            if (action == null) throw new ArgumentNullException("action");
            _pendingActions.Add(action);
        }

        public void CompletePendingAction(PendingActionCode code)
        {

        }

        public IList<PendingAction> PendingActions { get { return _pendingActions; } }
        #endregion

        #region Game activity
        public void RevealCard(Card card)
        {
            throw new NotImplementedException();
        }


        public void GainAction(int count = 1)
        {
            OnPlayEvent(new ActionGain(CurrentPlayer, count));
            _currentTurn.ActionsRemaining += count;
        }

        public void GainTreasure(int count)
        {
            OnPlayEvent(new TreasureGain(CurrentPlayer, count));
            _currentTurn.TreasureRemaining += count;
        }

        public Card DrawCard(int count = 1)
        {
            Card c = _currentTurn.Owner.Deck.Draw();
            OnPlayEvent(new CardDraw(CurrentPlayer, c));
            return c;
        }

        public Card GainCard(CardCode code)
        {
            if (!_supplies.ContainsKey(code)) throw new ArgumentOutOfRangeException("Supply piles do not include " + code.ToString());
            Card retval = null;

            CardContainer pile = _supplies[code];
            if (pile.Count > 0)
            {
                retval = pile.Draw();
            }

            OnPlayEvent(new CardGain(CurrentPlayer, retval));
            return retval;            
        }

        public void GainBuy(int count = 1)
        {
            OnPlayEvent(new BuyGain(CurrentPlayer, count));
            _currentTurn.BuysRemaining += count;
        }

        public void PlayAction(int cardId)
        {
            Card c = _cards[cardId];

            //
            // Make sure the action can be played.  i.e. it needs to be in the players hand
            //
            if (c.Container.Equals(_currentTurn.Owner.Hand))
            {
                OnPlayEvent(new ActionPlayed(CurrentPlayer, c));
                c.OnPlay();
            }
        }

        public void PutCardOnDeck(Card card)
        {
            OnPlayEvent(new PutCardOnDeck(CurrentPlayer, card));
            CurrentTurn.Owner.Deck.AddToTop(card);
        }
        #endregion

    }
}