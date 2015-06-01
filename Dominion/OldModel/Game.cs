using System;
using System.Collections.Generic;
using Autofac.Extras.NLog;
using Dominion.Constants;
using Dominion.Exceptions;
using Dominion.Interfaces;
using Dominion.Util;

namespace Dominion.OldModel
{
    /// <summary>
    /// Manages overall game state
    /// </summary>
    public class Game
    {
        private readonly ILogger _log;

        private readonly List<Player> _players = new List<Player>();
        private readonly SuppliesManager SupplyMan;
        private readonly TurnManager TurnMan;
        private readonly PendingEventsManager PendingMan;

        private Turn CurrentTurn { get { return TurnMan.Current; } }
        private readonly object _lck = new object();
                
        #region Constructors
        internal Game(IList<Player> players, IList<CardCode> supplies)
        {
            _players.AddRange(players);

            SupplyMan = new SuppliesManager(this, supplies);
            TurnMan = new TurnManager(this, true);
            PendingMan = new PendingEventsManager(this);
        }
        #endregion
        
        #region Player management
        public IList<Player> GetPlayers() { return new List<Player>(_players); }
        
        public Player CurrentPlayer { get { return CurrentTurn.Possessor ?? CurrentTurn.Owner; } }
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

        private PlayContext CreatePlayContext()
        {
            return new PlayContext(this, CurrentTurn, SupplyMan, PendingMan);
        }

        private void MoveCardToInPlay(Card c)
        {
            c.Container.Remove(c);
            CurrentTurn.CardsPlayed.Add(c);
            CurrentTurn.CardsInPlay.Add(c);
        }

        public void ReceivePendingEventResponse(PendingEventResponse response)
        {
            // TODO: Yes, this is not thread-safe in the least.  Will fix later.
            // TODO: I would *love* to hook C#5 continuations in with this...

            PendingMan.ReceivePendingEventResponse(response);
        }

        #region PlayCard
        internal IPlayCardResults PlayCard(Player player, Card c)
        {
            lock (_lck)
            {
                if (PendingMan.AwaitingResponses)
                    throw new YouCantDoThatException("Waiting for responses from players");

                if (!CurrentPlayer.Equals(player))
                    throw new YouCantDoThatException("It's not your turn");

                if (!(c.IsAction || c.IsTreasure))
                    throw new YouCantDoThatException("You can only play actions and treasures");

                PlayContext ctx = CreatePlayContext();

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
        }
        #endregion
    }
}