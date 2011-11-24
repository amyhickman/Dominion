using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Village : Card
    {
        public override CardCode CardCode { get { return CardCode.Village ; } }
        public override int Cost { get { return 3; } }
        public override CardSet Set { get { return CardSet.Base; } }

        public override void OnPlay(Turn turn)
        {
            turn.GainAction(2);
            turn.DrawCard();
        }
    }
}