using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;
using Dominion.Util;

namespace Dominion.Model
{
    public class PendingCardSelectionResponse : PendingEventResponse
    {
        public IList<Guid> Selections { get; private set; }

        public PendingCardSelectionResponse(Guid pendingEventId, IList<Guid> selections, bool declined = false)
            : base(pendingEventId)
        {
            Selections = new List<Guid>(selections);
            Declined = declined;
        }
    }
}
