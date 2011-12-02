using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Cards
{
    public class Militia : Card
    {
        public override int Cost
        {
            get { return 4; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override void OnPlay(PlayContext ctx)
        {
            ctx.GainTreasure(2);
            ctx.ForEachOtherPlayer(p => ctx.AddPendingEvent(new PendingCardSelection()
            {
                Target = p,
                CardOptions = new List<Card>(p.Hand),
                IsRequired = true,
                MinQty = p.Hand.Count - 3,
                MaxQty = p.Hand.Count - 3
            }));
        }
    }
}