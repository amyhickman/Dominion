using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Constants
{
    [Flags]
    public enum LocationCode
    {
        Hand = 1,
        Deck = 2,
        Discard = 4,

    }
}