using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Curse : Card
    {
        public override CardCode Code
        {
            get { return CardCode.Curse; }
        }

        public override int Cost
        {
            get { return 0; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override bool CanBeSupply
        {
            get
            {
                return false;
            }
        }

        public override int VictoryPoints
        {
            get
            {
                return -1;
            }
        }

        public override CardType Type
        {
            get
            {
                return CardType.Curse;
            }
        }
    }
}