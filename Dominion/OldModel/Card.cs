using System;
using System.Collections.Generic;
using Dominion.Constants;

namespace Dominion.OldModel
{
    public abstract class Card
    {
        public abstract int Cost { get; }
        public virtual int VictoryPoints { get { return 0; } }
        public virtual int TreasureValue { get { return 0; } }
        public virtual Uri ImageSource { get { return null; } }
        public abstract CardSet Set { get; }
        public virtual CardType Type { get { return CardType.Action; } }

        /// <summary>
        /// If this card is in the supply pile, it may require other cards be as well.  For instance, a witch requires curse cards.  If this card requires no other cards, an empty list is returned.
        /// </summary>
        public virtual IList<CardCode> RequiresInSupply { get { return new List<CardCode>(); } }

        public bool IsCardType(CardType t)
        {
            return (Type & t) == t;
        }

        public bool IsVictory { get { return IsCardType(CardType.Victory); } }
        public bool IsAction { get { return IsCardType(CardType.Action); } }
        public bool IsTreasure { get { return IsCardType(CardType.Treasure); } }
        public bool IsCurse { get { return IsCardType(CardType.Curse); } }
        public virtual bool CanBeSupply { get { return true; } }

        public virtual void OnPlay(PlayContext ctx) { }
        public virtual void OnDiscard(PlayContext ctx) { }
        public virtual void OnGain(PlayContext ctx) { }
    }
}