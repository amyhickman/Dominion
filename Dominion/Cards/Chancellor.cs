using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Cards
{
    public class Chancellor : Card
    {
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
            Game.AddPendingEvent(new PendingChoice(Game.CurrentPlayer, ChoiceCode.PutDeckOnDiscardPile));
        }
    }
}