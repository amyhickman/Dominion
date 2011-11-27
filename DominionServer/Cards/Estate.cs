﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Estate : Card
    {
        public override CardCode Code
        {
            get { return CardCode.Estate; }
        }

        public override int Cost
        {
            get { return 2; }
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