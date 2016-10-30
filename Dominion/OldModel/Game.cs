using System;
using System.Collections.Generic;
using Autofac.Extras.NLog;
using Dominion.Constants;
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

        private Turn CurrentTurn { get { return TurnMan.Current; } }
        private readonly object _lck = new object();
                
        #region Constructors
        internal Game(IList<Player> players, IList<CardCode> supplies)
        {
            _players.AddRange(players);

            SupplyMan = new SuppliesManager(this, supplies);
            TurnMan = new TurnManager(this, true);
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
            return new PlayContext(this, CurrentTurn, SupplyMan);
        }

        private void MoveCardToInPlay(Card c)
        {
            CurrentTurn.CardsPlayed.Add(c);
            CurrentTurn.CardsInPlay.Add(c);
        }


    }
}