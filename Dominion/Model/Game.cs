using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Util;
using log4net;
using System.Diagnostics;
using Dominion.Interfaces;
using Dominion.Constants;
using Dominion.Exceptions;

namespace Dominion.Model
{
    public class Game
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Game));

        private readonly List<Player> _players = new List<Player>();
        private readonly bool _started = false;
        private readonly CardManager CardMan;
        private readonly SuppliesManager SupplyMan;
        private readonly TurnManager TurnMan;
        private readonly Dictionary<Guid, PendingEvent> _pending = new Dictionary<Guid, PendingEvent>();

        private Turn CurrentTurn { get { return TurnMan.Current; } }
        private readonly object _lck = new object();
                
        #region Constructors
        internal Game(IList<Player> players, IList<CardCode> supplies)
        {
            _players.AddRange(players);

            CardMan = new CardManager(this);
            SupplyMan = new SuppliesManager(this, CardMan, supplies);
            TurnMan = new TurnManager(this, true);

            foreach (var p in _players)
            {
                p.Deck.AddRange(CardMan.CreateCards(CardCode.Copper, 7));
                p.Deck.AddRange(CardMan.CreateCards(CardCode.Estate, 3));
                p.Deck.Shuffle();
                p.Hand.AddRange(p.Deck.Draw(5));
            }

            _started = true;
        }
        #endregion
        
        #region Player management
        public IList<Player> GetPlayers() { return new List<Player>(_players); }
        
        public Player CurrentPlayer { get { return CurrentTurn.Owner; } }
        #endregion

        #region Event notifications
        public void NotifyPlayers(Action<Player> notification) { _players.ForEach(p=> notification(p)); }

        public void NotifyPlayers(Action<Player> currentPlayerNotifier, Action<Player> otherPlayerNotifier)
        {
            currentPlayerNotifier(CurrentPlayer);
            foreach (var p in _players)
            {
                if (p.Equals(CurrentPlayer))
                    continue;
                otherPlayerNotifier(p);
            }
        }
        #endregion

        #region Game event management
        internal void AddPendingEvent(PendingEvent pending)
        {
            _pending.Add(pending.Id, pending);
        }
        
        private void ReceivePendingEventResponse(PendingEventResponse response)
        {
            PendingEvent request = _pending[response.PendingEventId];

            if (request.IsSatisfiedByResponse(response))
                request.OnResponse(response);
            else if (request is PendingCardSelection)
            {
                SendPendingRequest((PendingCardSelection)request);
            }
            else if (request is PendingChoice)
            {
                SendPendingRequest((PendingChoice)request);
            }
        }

        private void SendPendingRequest(PendingCardSelection pending)
        {
            pending.Player.OnCardSelectionRequested(pending);
        }

        private void SendPendingRequest(PendingChoice pending)
        {
            pending.Player.OnChoiceRequested(pending);
        }

        #endregion

        private PlayContext CreatePlayContext()
        {
            return new PlayContext(this, CurrentTurn, SupplyMan);
        }

        private void MoveCardToInPlay(Card c)
        {
            c.Container.Remove(c);
            CurrentTurn.CardsPlayed.Add(c);
            CurrentTurn.CardsInPlay.Add(c);
        }

        #region PlayCard
        public IPlayCardResults PlayCard(Player player, Card c)
        {
            lock (_lck)
            {
                if (!CurrentPlayer.Equals(player))
                    throw new YouCantDoThatException("It's not your turn");

                return PlayCardInternal(player, c);
            }
        }

        private IPlayCardResults PlayCardInternal(Player player, Card c)
        {
            PlayContext ctx = CreatePlayContext();

            if (!(c.IsAction || c.IsTreasure))
                throw new YouCantDoThatException("You can only play actions and treasures");

            if (c.IsAction)
            {
                if (CurrentTurn.ActionsRemaining <= 0)
                {
                    CurrentTurn.Phase = Phases.Buy;
                    throw new YouCantDoThatException("You have no actions remaining");
                }
                if (CurrentTurn.Phase != Phases.Action)
                    throw new YouCantDoThatException("You can only play actions during your action phase");

                CurrentTurn.ActionsRemaining--;
                MoveCardToInPlay(c);
                c.OnPlay(ctx);
                return ctx.GetResults();
            }
            else if (c.IsTreasure)
            {
                if (CurrentTurn.Phase == Phases.Action)
                    CurrentTurn.Phase = Phases.Buy;

                if (CurrentTurn.Phase == Phases.Cleanup)
                    throw new YouCantDoThatException("You cannot play treasures during your cleanup phase");

                MoveCardToInPlay(c);
                CurrentTurn.TreasureRemaining += c.TreasureValue;
                c.OnPlay(ctx);
                return ctx.GetResults();
            }
            else
                throw new YouCantDoThatException("You cannot play victory cards");

        }
        #endregion
    }
}