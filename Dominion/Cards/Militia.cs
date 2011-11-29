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

        public override void OnPlay()
        {
            Game.GainTreasure(2);
            Game.ForEachOtherPlayer(p => Game.AddPendingEvent(new PendingCardSelection(p, p.Hand)
            {
                IsRequired = true,
                MinQty = p.Hand.Count - 3,
                MaxQty = p.Hand.Count - 3
            }));
        }
    }
}