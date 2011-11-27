using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.GameEventModel
{
    [Flags]
    public enum PendingActionCode
    {
        RevealVictoryCard,
        RevealHandWithNoVictoryCards,
        ShuffleDeckIntoDiscardPile,
        PlayActionCard,
        PlayTreasuryCard
    }
}