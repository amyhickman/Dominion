using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Interfaces;
using Dominion.Constants;

namespace Dominion.Model
{
    public abstract class Card : ICardMetadata
    {
        public Guid Id { get; private set; }
        public CardContainer Container { get; set; }

        private readonly string _name;
        private readonly CardCode _code;

        public virtual string Name { get { return _name; } }
        public virtual CardCode Code { get { return _code; } }
        public abstract int Cost { get; }
        public virtual int VictoryPoints { get { return 0; } }
        public virtual int TreasureValue { get { return 0; } }
        public virtual Uri ImageSource { get { return null; } }
        public abstract CardSet Set { get; }
        public virtual CardType Type { get { return CardType.Action; } }
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

        public Card()
        {
            _name = this.GetType().Name;
            _code = (CardCode)Enum.Parse(typeof(CardCode), _name);
            Id = Guid.NewGuid();
        }
    }
}