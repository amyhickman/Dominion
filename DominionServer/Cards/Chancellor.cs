using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Chancellor : Card
    {
        public override CardCode CardCode
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

        public override void OnPlay(Turn turn)
        {
            turn.GainCoin(2);
            turn.Game.AddPendingAction(new PendingAction()
            {
                Player = turn.Owner,
                Codes = PendingActionCodes.ShuffleDeckIntoDiscardPile,
                IsRequired = false
            });
        }
    }
}