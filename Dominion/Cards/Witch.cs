using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;

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

        public override IList<CardCode>  RequiresInSupply
        {
            get { return new List<CardCode>() { CardCode.Curse }; }
        }

        public override void OnPlay(PlayContext ctx)
        {
            ctx.DrawCards(2);

            //
            // each other player gains a curse card
            //
            ctx.ForEachOtherPlayer(p =>
            {
                Card curse = ctx.GainCard(p, CardCode.Curse);
                if (curse != null)
                    p.DiscardPile.Add(curse);
            });
        }
    }
}