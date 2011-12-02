using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Cards
{
    public class ThroneRoom : Card
    {
        public override string Name
        {
            get { return "Throne Room"; }
        }

        public override CardCode Code
        {
            get { return CardCode.ThroneRoom; }
        }

        public override int Cost
        {
            get { return 4; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; ; }
        }

        public override void OnPlay(PlayContext ctx)
        {
            ctx.AddPendingEvent(new PendingCardSelection()
                {
                    Target = ctx.Actor, 
                    CardOptions = new List<Card>(ctx.Owner.Hand.Where(c => c.IsAction)),
                    IsRequired = true, 
                    MinQty = 1, 
                    MaxQty = 1, 
                    OnFulfillment = OnCardsSelected
                });
        }
        
        private void OnCardsSelected(PendingEventResponse response)
        {
            
        }
    }
}