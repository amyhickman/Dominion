using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Model
{
    public class PendingChoiceResponse : PendingEventResponse
    {
        public PendingChoiceResponse(Guid pendingEventId, bool declined)
            : base(pendingEventId)
        {
            Declined = declined;
        }
    }
}
