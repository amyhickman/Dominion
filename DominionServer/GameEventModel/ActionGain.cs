using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class ActionGain : GameEvent
    {
        public int Count { get; set; }

        public ActionGain(Player actor, int count)
        {
            ByPlayer = actor;
            Count = count;
        }
    }
}