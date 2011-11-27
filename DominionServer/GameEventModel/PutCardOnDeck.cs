using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class PutCardOnDeck : CardEvent
    {
        public PutCardOnDeck(Player player, Card card)
        {
            ByPlayer = player;
            Card = card;
        }
    }
}