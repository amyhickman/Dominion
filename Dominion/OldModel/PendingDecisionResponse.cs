using System;

namespace Dominion.OldModel
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
