using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Dominion.Interfaces;

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

        public Player(IPrincipal principal, IGameObserver observer)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");
            if (observer == null)
                throw new ArgumentNullException("observer");

            Principal = principal;
            Observer = observer;
            Hand = new CardContainer(this);
            Deck = new CardContainer(this);
            DiscardPile = new CardContainer(this);
        }

        public IGameObserver Observer { get; private set; }
    }
}