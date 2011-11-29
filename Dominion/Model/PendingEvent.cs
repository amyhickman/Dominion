using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Model
{
    public abstract class PendingEvent
    {
        public Guid Id { get; private set; }
        public Player Player { get; set; }
        public Action<PendingEventResponse> OnResponse { get; set; }

        public PendingEvent()
        {
            Id = Guid.NewGuid();
        }

        public abstract bool IsSatisfiedByResponse(PendingEventResponse response);
    }
}
