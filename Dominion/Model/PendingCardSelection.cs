using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Model
{
    public class PendingCardSelection : PendingEvent
    {
        public int MinQty { get; set; }
        public int MaxQty { get; set; }
        public List<Card> CardOptions { get; set; }
        public bool IsRequired { get; set; }

        public override bool IsSatisfiedByResponse(PendingEventResponse response)
        {
            PendingCardSelectionResponse cardSelectionResponse = response as PendingCardSelectionResponse;
            if (cardSelectionResponse == null)
                return false;

            if (IsRequired && cardSelectionResponse.Declined)
                return false;

            if (cardSelectionResponse.Selections.Count < MinQty || cardSelectionResponse.Selections.Count > MaxQty)
                return false;

            List<Card> options = new List<Card>(CardOptions);
            foreach (var c in cardSelectionResponse.Selections)
            {
                if (!options.Contains(c))
                    return false;

                options.Remove(c);
            }

            return true;
        }
    }
}