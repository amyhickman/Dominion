using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Constants;

namespace Dominion.Model
{
    public class PendingChoice : PendingEvent
    {
        public ChoiceCode Choice { get; set; }
        
        public override bool IsSatisfiedByResponse(PendingEventResponse response)
        {
            PendingChoiceResponse choiceResponse = response as PendingChoiceResponse;
            return (choiceResponse != null);
        }
    }
}
