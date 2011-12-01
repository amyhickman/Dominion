using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Model
{
    public class PendingDecisionResponse : PendingEventResponse
    {
        public PendingDecisionResponse(Guid pendingEventId, bool declined)
            : base(pendingEventId)
        {
            Declined = declined;
        }
    }
}
