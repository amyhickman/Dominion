using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Cards
{
    public class Chapel : Card
    {
        public override int Cost
        {
            get { return 2; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override void OnPlay(PlayContext ctx)
        {
            ctx.AddPendingEvent(new PendingCardSelection()
            {
                Player = ctx.Actor,
                CardOptions = new List<Card>(ctx.Owner.Hand),
                IsRequired = false,
                MinQty = 0,
                MaxQty = 4
            });
        }
    }
}