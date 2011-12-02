using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Model
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
