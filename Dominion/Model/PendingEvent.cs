using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Model
{
    public abstract class PendingEvent
    {
        public Guid Id { get; private set; }
        public Player Player { get; private set; }
        public Action<PendingEventResponse> OnResponse { get; set; }

        public PendingEvent(Player player)
        {
            Id = Guid.NewGuid();
            Player = player;
        }

        public abstract bool IsSatisfiedByResponse(PendingEventResponse response);
    }
}
