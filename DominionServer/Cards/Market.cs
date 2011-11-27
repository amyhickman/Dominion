using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

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

        public override void OnPlay()
        {
            Game.GainAction(1);
            Game.DrawCard(1);
            Game.GainBuy(1);
            Game.GainTreasure(1);
        }
    }
}