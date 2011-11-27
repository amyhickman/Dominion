using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.PendingEventModel;

namespace Dominion.Cards
{
    public class ThroneRoom : Card
    {
        public override string Name
        {
            get { return "Throne Room"; }
        }

        public override CardCode Code
        {
            get { return CardCode.ThroneRoom; }
        }

        public override int Cost
        {
            get { return 4; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; ; }
        }

        public override void OnPlay()
        {
            Game.AddPendingAction(new ChooseActionCardPlayItNTimes(Game.CurrentPlayer, 2, true));
        }
        
    }
}