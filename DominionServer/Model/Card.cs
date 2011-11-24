using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Model
{
    public abstract class Card
    {
        public int CardId { get; set; }
        public CardContainer Container { get; set; }

        public virtual string Name { get { return this.GetType().Name; } }
        public abstract CardCode CardCode { get; }
        public abstract int Cost { get; }
        public virtual Uri ImageSource { get { return null; } }
        public abstract CardSet Set { get; }

        public virtual bool IsTreasure { get { return false; } }
        public virtual bool IsVictory { get { return false; } }
        public virtual bool IsAction { get { return true; } }

        public virtual void OnPlay(Turn turn) { }
        public virtual void OnDiscard(Turn turn) { }
        public virtual void OnGain(Turn turn) { }
    }
}