using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Cards
{
    public class Woodcutter : Card
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
            ctx.GainBuy();
            ctx.GainTreasure(2);
        }
    }
}