using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Dominion.Model
{
    public class Player
    {
        public GenericIdentity Identity { get; private set; }

        public CardContainer Hand { get; private set; }
        public CardContainer Deck { get; private set; }
        public CardContainer DiscardPile { get; private set; }
        public int PlayerId { get; set; }
        public int VictoryPoints { get; set; }

        public List<PendingActionCodes> RequiredActions { get; private set; }

        public Player(GenericIdentity identity)
        {
            Identity = identity;
            Hand = new CardContainer();
            Deck = new CardContainer();
            DiscardPile = new CardContainer();
            RequiredActions = new List<PendingActionCodes>();
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