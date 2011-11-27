using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Duchy : Card
    {

        public override CardCode Code
        {
            get { return CardCode.Duchy; }
        }

        public override int Cost
        {
            get { return 5; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override CardType Type
        {
            get
            {
                return CardType.Victory;
            }
        }

        public override int VictoryPoints
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