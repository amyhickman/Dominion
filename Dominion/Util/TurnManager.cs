using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;
using log4net;

namespace Dominion.Util
{
    public class TurnManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(TurnManager));
        private readonly List<Turn> _turns = new List<Turn>(); 
        
        public Game Game { get; private set; }
        public Turn Current { get; private set; }

        public TurnManager(Game game, bool randomizeOrder = true)
        {
            Game = game;

            var players = game.GetPlayers();
            foreach (var p in players)
            {
                _turns.Add(new Turn(p));
            }

            if (randomizeOrder)
                _turns.Shuffle();
        }

        public void Next()
        {
            Current = _turns[0];
            _turns.RemoveAt(0);

            if (Current.IsRepeatable)
                _turns.Add(new Turn(Current.Owner) { TurnNumber = Current.TurnNumber + 1 });
        }

        public void TakeExtraTurn(Turn turn)
        {
            _turns.Insert(0, turn);
        }
    }
}
