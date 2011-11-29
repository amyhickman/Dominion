using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Constants
{
    [Flags]
    public enum CardType
    {
        Action = 1,
        Victory = 2,
        Treasure = 4,
        Curse = 8
    }
}