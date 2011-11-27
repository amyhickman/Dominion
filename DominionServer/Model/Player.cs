using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Dominion.Model
{
    public class Player
    {
        public string Nickname { get; set; }
        public IPrincipal Principal { get; private set; }

        public CardContainer Hand { get; private set; }
        public CardContainer Deck { get; private set; }
        public CardContainer DiscardPile { get; private set; }
        public int Id { get; set; }
        public int VictoryPoints { get; set; }

        public Player(IPrincipal principal)
        {
            Principal = principal;
            Hand = new CardContainer();
            Deck = new CardContainer();
            DiscardPile = new CardContainer();
        }


        internal void RevealCard(Card card)
        {
            throw new NotImplementedException();
        }

        internal void RevealHand()
        {
            throw new NotImplementedException();
        }
    }
}