using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class PendingAction
    {
        public bool IsRequired { get; set; }
        public PendingActionCode Codes { get; set; }
        public Player Player { get; set; }

    }
}