using System;

namespace Dominion.OldModel
{
    public abstract class PendingEvent
    {
        public Guid Id { get; private set; }
        public Player Target { get; set; }

        public PendingEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}
