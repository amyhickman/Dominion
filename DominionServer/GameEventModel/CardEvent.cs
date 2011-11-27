using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Model;

namespace Dominion.GameEventModel
{
    public abstract class CardEvent : GameEvent
    {
        public Card Card { get; set; }
        public List<Player> VisibleTo { get; private set; }

        public CardEvent()
        {
            VisibleTo = new List<Player>();
        }

        public CardEvent(Player actor) : base(actor)
        {
            VisibleTo = new List<Player>();
        }


    }
}