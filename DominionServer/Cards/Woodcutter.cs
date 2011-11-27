using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Woodcutter : Card
    {
        public override CardCode Code
        {
            get { return CardCode.Woodcutter; }
        }

        public override int Cost
        {
            get { return 3; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override void OnPlay()
        {
            Game.GainBuy();
            Game.GainTreasure(2);
        }
    }
}