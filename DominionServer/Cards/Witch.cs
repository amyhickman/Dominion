using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.Cards
{
    public class Witch : Card
    {
        public override int Cost
        {
            get { return 5; }
        }

        public override CardSet Set
        {
            get { return CardSet.Base; }
        }

        public override bool RequiresCurseSupply
        {
            get { return true; }
        }

        public override void OnPlay()
        {
            Game.DrawCards(2);

            //
            // each other player gains a curse card
            //
            foreach (var p in Game.Players)
            {
                if (p.Equals(Game.CurrentPlayer))
                    continue;

                Card curse = Game.GainCard(p, CardCode.Curse);
                if (curse != null)
                    p.DiscardPile.Add(curse);
            }
        }
    }
}