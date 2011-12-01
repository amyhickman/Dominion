using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Constants;

namespace Dominion.Model
{
    public class PendingDecision : PendingEvent
    {
        public DecisionCode Choice { get; set; }
        
        public override bool IsSatisfiedByResponse(PendingEventResponse response)
        {
            PendingDecisionResponse choiceResponse = response as PendingDecisionResponse;
            return (choiceResponse != null);
        }
    }
}
