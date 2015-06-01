using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;
using Dominion.OldModel;

namespace Dominion.Cards
{
    public class Market : Card
    {
        public override int Cost
        {
            get { return 5; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override void OnPlay(PlayContext ctx)
        {
            ctx.GainAction(1);
            ctx.DrawCards(1);
            ctx.GainBuy(1);
            ctx.GainTreasure(1);
        }
    }
}