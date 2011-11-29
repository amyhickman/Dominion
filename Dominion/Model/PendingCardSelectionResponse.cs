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
        public IList<Card> Selections { get; private set; }

        public PendingCardSelectionResponse(Guid pendingEventId, IList<Card> selections, bool declined = false)
            : base(pendingEventId)
        {
            Selections = new List<Card>(selections);
            Declined = declined;
        }
    }
}
