using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.PendingEventModel;

namespace Dominion.Cards
{
    public class Chapel : Card
    {
        public override int Cost
        {
            get { return 2; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override void OnPlay()
        {
            Game.AddPendingAction(new TrashUpToNCards() { Count = 4, FromLocation = LocationCode.Hand, IsRequired = true, Player = Game.CurrentPlayer });
        }
    }
}