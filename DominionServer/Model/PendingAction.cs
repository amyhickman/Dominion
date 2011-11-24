using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Model
{
    public class PendingAction
    {
        public bool IsRequired { get; set; }
        public PendingActionCodes Codes { get; set; }
        public Player Player { get; set; }

    }
}