using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Cellar : Card
    {
        public override CardCode CardCode
        {
            get { return CardCode.Cellar; }
        }

        public override int Cost
        {
            get { return 2; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }
    }
}