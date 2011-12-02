using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Constants;

namespace Dominion.Model
{
    public class PendingDecision : PendingEvent
    {
        public DecisionCode Decision { get; set; }
        public Func<PendingDecisionResponse, bool> OnResponse { get; set; }

        public PendingDecision(Player target, DecisionCode choice)
        {
            Target = target;
            Decision = choice;
        }

        public PendingDecision(Player target, DecisionCode decision, Func<PendingDecisionResponse, bool> onResponse)
        {
            OnResponse = onResponse;
            Target = target;
            Decision = decision;
        }
    }
}
