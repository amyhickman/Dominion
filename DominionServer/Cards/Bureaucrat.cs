using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;


namespace Dominion.Cards
{
    public class Bureaucrat : Card
    {
        public override CardCode CardCode { get { return Model.CardCode.Bureaucrat; } }
        public override int Cost { get { return 4; } }
        public override CardSet Set { get { return CardSet.Base; } }

        public override void OnPlay(Turn turn)
        {
            var silver = turn.GainCard(CardCode.Silver);
            turn.Owner.Deck.AddToTop(silver);

            foreach (var player in turn.Game.Players)
            {
                if (player.Equals(turn.Owner))
                    continue;

                // each other player must reveal a victory card from his hand or reveal a hand with no victory cards
                turn.Game.AddPendingAction(new PendingAction() 
                { 
                    Player = player,
                    Codes = PendingActionCodes.RevealVictoryCard | PendingActionCodes.RevealHandWithNoVictoryCards,
                    IsRequired = true
                });
            }
        }
    }
}