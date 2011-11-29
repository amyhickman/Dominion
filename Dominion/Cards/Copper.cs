﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Cards
{
    public class Copper : Card
    {
        public override int Cost
        {
            get { return 0; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override CardType Type
        {
            get
            {
                return CardType.Treasure;
            }
        }

        public override int TreasureValue
        {
            get
            {
                return 1;
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