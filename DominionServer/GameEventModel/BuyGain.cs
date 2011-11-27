using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class BuyGain : GameEvent
    {
        public int Count { get; set; }

        public BuyGain(Player actor, int count)
        {
            ByPlayer = actor;
            Count = count;
        }
    }
}