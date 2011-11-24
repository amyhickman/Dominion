using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Model
{
    [Flags]
    public enum PendingActionCodes
    {
        RevealVictoryCard,
        RevealHandWithNoVictoryCards,
        ShuffleDeckIntoDiscardPile
    }
}