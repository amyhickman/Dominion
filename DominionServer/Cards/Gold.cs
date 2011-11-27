using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Gold : Card
    {
        public override CardCode Code
        {
            get { return CardCode.Gold; }
        }

        public override int Cost
        {
            get { return 6; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override CardType Type { get { return CardType.Treasure; } }

        public override int TreasureValue
        {
            get
            {
                return 3;
            }
        }

        public override bool CanBeSupply
        {
            get
            {
                return false;
            }
        }
    }
}