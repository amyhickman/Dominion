using System;

namespace Dominion.OldModel
{
    public abstract class PendingEventResponse
    {
        public Guid PendingEventId { get; private set; }
        public bool Declined { get; set; }

        public PendingEventResponse(Guid pendingEventId)
        {
            PendingEventId = pendingEventId;

        }
    }
}
