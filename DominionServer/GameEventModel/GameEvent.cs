using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public abstract class GameEvent
    {
        public Player ByPlayer { get; set; }

        public GameEvent() { }
        public GameEvent(Player actor)
        {
            ByPlayer = actor;
        }
    }

}