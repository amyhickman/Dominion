using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.PendingEventModel
{
    public class ChooseActionCardPlayItNTimes : PendingAction
    {
        public int Count { get; set; }

        public ChooseActionCardPlayItNTimes(Player target, int count, bool isRequired = false)
        {
            Count = count;
            Player = target;
            IsRequired = isRequired;
        }
    }
}