using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.PendingEventModel
{
    public class RevealACard : PendingAction
    {
        public List<Card> CardChoices { get; set; }
        public int Count { get; set; }

        public RevealACard(Player target, List<Card> choices, int count)
        {
            CardChoices = choices;
            Count = count;
            Player = target;
        }
    }
}