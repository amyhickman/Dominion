using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;
using Dominion.GameEventModel;


namespace Dominion.Cards
{
    public class Bureaucrat : Card
    {
        public override CardCode Code { get { return Model.CardCode.Bureaucrat; } }
        public override int Cost { get { return 4; } }
        public override CardSet Set { get { return CardSet.Base; } }

        public override void OnPlay()
        {
            var silver = Game.GainCard(CardCode.Silver);
            Game.PutCardOnDeck(silver);

            foreach (var player in Game.Players)
            {
                if (player.Equals(Game.CurrentPlayer))
                    continue;

                // each other player must reveal a victory card from his hand or reveal a hand with no victory cards
                Game.AddPendingAction(new PendingAction() 
                { 
                    Player = player,
                    Codes = PendingActionCode.RevealVictoryCard | PendingActionCode.RevealHandWithNoVictoryCards,
                    IsRequired = true
                });
            }
        }
    }
}