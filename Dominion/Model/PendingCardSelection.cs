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

        public Func<PendingCardSelectionResponse, List<Guid>, bool> OnResponse { get; set; }

        public List<Card> GetValidSelections(PendingCardSelectionResponse response)
        {
            return (from co in CardOptions
                    where response.Selections.Contains(co.Id)
                    select co).ToList();
        }

        public bool ValidateResponse(PendingCardSelectionResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            if (IsRequired && response.Declined)
                return false;

            if (response.Declined)
                return true;

            var goodSelections = GetValidSelections(response);
            if (goodSelections.Count < MinQty || goodSelections.Count > MaxQty)
                return false;



            return true;
        }
    }
}