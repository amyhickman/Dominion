using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;
using Dominion.OldModel;

namespace Dominion.Cards
{
    public class Village : Card
    {
        public override int Cost { get { return 3; } }
        public override CardSet Set { get { return CardSet.Base; } }

        public override void OnPlay(PlayContext ctx)
        {
            ctx.GainAction(2);
            ctx.DrawCards(1);
        }
    }
}