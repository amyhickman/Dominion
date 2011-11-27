using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class CardReveal : CardEvent
    {
        public CardReveal(Player actor, Card card)
        {
            ByPlayer = actor;
            Card = card;
        }
    }
}