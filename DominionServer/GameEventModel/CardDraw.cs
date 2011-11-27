using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class CardDraw : CardEvent
    {
        public CardDraw(Player actor, Card card)
        {
            ByPlayer = actor;
            Card = card;
        }
    }
}