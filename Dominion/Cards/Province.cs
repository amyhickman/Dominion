using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;
using Dominion.OldModel;

namespace Dominion.Cards
{
    public class Province : Card
    {
        public override int Cost
        {
            get { return 8; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override int VictoryPoints
        {
            get
            {
                return 6;
            }
        }

        public override CardType Type
        {
            get
            {
                return CardType.Victory;
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