using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace Dominion.Model
{
    public class Game
    {
        private Dictionary<int, CardContainer> _containers = new Dictionary<int, CardContainer>();
        private Dictionary<int, Card> _cards = new Dictionary<int, Card>();
        private Dictionary<CardCode, CardContainer> _supplies = new Dictionary<CardCode, CardContainer>();
        private List<Player> _players = new List<Player>();

        private Queue<Turn> _turns = new Queue<Turn>();
        private Turn _currentTurn { get; set; }

        private CardContainer _cardsInPlay = new CardContainer();
        private CardContainer _cardsPlayed = new CardContainer();
        private List<PendingAction> _pendingEvents = new List<PendingAction>();

        private CardFactory _cardFactory = new CardFactory();
        private Random _rand = new Random();

        public event Action<PlayEvent> OnPlayEvent;
        
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
                _turns.Enqueue(new Turn(_currentTurn.Owner, true));

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
            if (!_cards.ContainsKey(c.CardId))
                _cards.Add(c.CardId, c);
        }

        public void MoveCard(Card c, CardContainer targetContainer)
        {
            c.Container.Remove(c);
            targetContainer.Add(c);
            c.Container = targetContainer;
        }
        #endregion

        #region Supply management
        private void GenerateSupplies(params CardCode[] codes)
        {
            foreach (CardCode c in codes)
            {
                CreateSupplyPile(c, 10);
            }

            while (_supplies.Count < 10)
            {
                CardCode c = (CardCode)_rand.Next(0, Enum.GetValues(typeof(CardCode)).Cast<int>().Max());
                CreateSupplyPile(c, 10);
            }
        }

        private void CreateSupplyPile(CardCode c, int quantity)
        {
            CardContainer pile = new CardContainer();
            RegisterContainer(pile);

            for (int i = 0; i < quantity; i++)
            {
                Card card = _cardFactory.GetCard(c);
                pile.Add(card);
                RegisterCard(card);
            }
        }

        #endregion

        #region Player management
        public void AddPlayer(Player p)
        {
            if (p == null)
                throw new ArgumentNullException("p");
            
            _players.Add(p);
            RegisterContainer(p.Deck);
            RegisterContainer(p.DiscardPile);
            RegisterContainer(p.Hand);
        }
        #endregion

        public Game()
        {
            _currentTurn = null;

            RegisterContainer(_cardsInPlay);
            RegisterContainer(_cardsPlayed);
        }

        internal void RevealCard(Card card)
        {
            throw new NotImplementedException();
        }

        public void AddPendingAction(PendingAction action)
        {
            if (action == null) throw new ArgumentNullException("action");

            _pendingEvents.Add(action);
        }

    }
}