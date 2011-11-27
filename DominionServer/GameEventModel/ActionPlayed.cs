using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public class ActionPlayed : CardEvent
    {
        public ActionPlayed(Player actor, Card action)
        {
            Card = action;
            ByPlayer = actor;
        }
    }
}