using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Model
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
