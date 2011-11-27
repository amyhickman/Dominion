using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.GameEventModel;

namespace Dominion.Cards
{
    public class Chancellor : Card
    {
        public override CardCode Code
        {
            get { return CardCode.Chancellor; }
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
            Game.GainTreasure(2);
            Game.AddPendingAction(new PendingAction()
            {
                Player = Game.CurrentPlayer,
                Codes = PendingActionCode.ShuffleDeckIntoDiscardPile,
                IsRequired = false
            });
        }
    }
}