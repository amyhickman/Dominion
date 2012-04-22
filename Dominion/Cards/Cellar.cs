using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Cards
{
    public class Cellar : Card
    {
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