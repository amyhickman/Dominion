using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class CardBuy : CardEvent
    {
        public CardBuy(Player byPlayer, Card card)
        {
            ByPlayer = byPlayer;
            Card = card;
        }
    }
}