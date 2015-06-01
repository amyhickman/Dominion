using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;
using Dominion.OldModel;

namespace Dominion.Cards
{
    public class Chancellor : Card
    {
        public override int Cost
        {
            get { return 3; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override void OnPlay(PlayContext ctx)
        {
            ctx.GainTreasure(2);
            ctx.AddPendingEvent(new PendingDecision(ctx.Actor, DecisionCode.PutDeckOnDiscardPile));
        }
    }
}