using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.PendingEventModel
{
    public abstract class PendingAction
    {
        public bool IsRequired { get; set; }
        public Player Player { get; set; }

    }
}