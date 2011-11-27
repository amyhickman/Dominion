using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.PendingEventModel;

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
            Game.ForEachOtherPlayer(p => Game.AddPendingAction(new DiscardDownToNCards() { Count = 3, Player = p, IsRequired = true }));
        }
    }
}