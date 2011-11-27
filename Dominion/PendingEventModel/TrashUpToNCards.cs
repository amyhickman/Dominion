using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.PendingEventModel
{
    public class TrashUpToNCards : PendingAction
    {
        public int Count { get; set; }
        public LocationCode FromLocation { get; set; }
    }
}