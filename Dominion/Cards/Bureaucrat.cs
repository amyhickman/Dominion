using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.Constants;


namespace Dominion.Cards
{
    public class Bureaucrat : Card
    {
        public override int Cost { get { return 4; } }
        public override CardSet Set { get { return CardSet.Base; } }

        public override void OnPlay()
        {
            var silver = Game.GainCard(CardCode.Silver);
            Game.PutCardOnDeck(silver);

            Game.ForEachOtherPlayer(p =>
                {
                    // each other player must reveal a victory card from his hand or reveal a hand with no victory cards
                    var victoryCodes = p.Hand.Where(c=> c.IsVictory).Select(c => c.Code).Distinct().ToList();

                    switch (victoryCodes.Count)
                    {
                        case 0:
                            Game.RevealHand(p);
                            break;
                        case 1:
                            Game.RevealCard(p, p.Hand.Where(c => c.Code == victoryCodes[0]).FirstOrDefault());
                            break;
                        default:
                            var victories = p.Hand.Where(c => victoryCodes.Contains(c.Code)).ToList();
                            Game.AddPendingEvent(new PendingCardSelection(p, victories) { IsRequired = true, MinQty = 1, MaxQty = 1 });
                            break;
                    }
                    
                });
        }
    }
}