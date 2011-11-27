using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class TreasureGain : GameEvent
    {
        public int Amount { get; set; }

        public TreasureGain(Player actor, int amount)
        {
            ByPlayer = actor;
            Amount = amount;
        }
    }
}