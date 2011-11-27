using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.PendingEventModel
{
    public class PutDeckOnDiscardPile : PendingAction
    {
        public PutDeckOnDiscardPile(Player p, bool isRequired)
        {
            Player = p;
            IsRequired = isRequired;
        }
    }
}