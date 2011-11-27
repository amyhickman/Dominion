using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Model
{
    public abstract class Card
    {
        public int Id { get; set; }
        public CardContainer Container { get; set; }

        public virtual string Name { get { return this.GetType().Name; } }
        public abstract CardCode Code { get; }
        public abstract int Cost { get; }
        public virtual Uri ImageSource { get { return null; } }
        public abstract CardSet Set { get; }
        public virtual CardType Type { get { return CardType.Action; } }
        public Game Game { get; set; }

        public bool IsCardType(CardType t)
        {
            return (Type & t) == t;
        }

        public bool IsVictory { get { return IsCardType(CardType.Victory); } }
        public bool IsAction { get { return IsCardType(CardType.Action); } }
        public bool IsTreasure { get { return IsCardType(CardType.Treasure); } }

        public virtual void OnPlay() { }
        public virtual void OnDiscard() { }
        public virtual void OnGain() { }
    }
}