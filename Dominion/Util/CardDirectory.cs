using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Interfaces;
using Dominion.Model;
using System.Reflection;
using System.Linq.Expressions;
using Dominion.Constants;

namespace Dominion.Util
{
    public static class CardDirectory
    {
        private class CardMetadata
        {
            public Func<Card> Factory { get; set; }
            public ICardMetadata Meta { get; set; }

            public CardMetadata(Card c, Func<Card> fact) { Factory = fact; Meta = c; }
        }

        private class Setmeta
        {
            public List<ICardMetadata> Supplies { get; set; }
            public List<ICardMetadata> Cards { get; set; }

            public Setmeta()
            {
                Supplies = new List<ICardMetadata>();
                Cards = new List<ICardMetadata>();
            }
        }

        private static readonly Dictionary<CardSet, Setmeta> _sets = new Dictionary<CardSet, Setmeta>();
        private static readonly Dictionary<CardCode, CardMetadata> _cards = new Dictionary<CardCode, CardMetadata>();

        public static IList<ICardMetadata> GetCardsInSet(CardSet set)
        {
            if (_sets.ContainsKey(set))
                return new List<ICardMetadata>(_sets[set].Cards);

            return new List<ICardMetadata>();
        }

        public static ICardMetadata GetCardMeta(CardCode code)
        {
            return _cards[code].Meta;
        }

        public static IList<ICardMetadata> GetSuppliesInSet(CardSet set)
        {
            if (_sets.ContainsKey(set))
                return new List<ICardMetadata>(_sets[set].Supplies);

            return new List<ICardMetadata>();
        }

        public static Card CreateCard(CardCode code)
        {
            return _cards[code].Factory();
        }

        public static IList<Card> CreateCards(IEnumerable<CardCode> codes)
        {
            List<Card> retval = new List<Card>();
            foreach (var c in codes)
                retval.Add(CreateCard(c));
            return retval;
        }

        public static IList<Card> CreateCards(CardCode code, int count)
        {
            List<Card> retval = new List<Card>();
            while (count > 0)
            {
                retval.Add(CreateCard(code));
                count--;
            }
            return retval;
        }

        static CardDirectory()
        {
            Assembly assembly = typeof(Card).Assembly;
            foreach (var t in assembly.GetTypes().Where(x => typeof(Card).IsAssignableFrom(x)))
            {
                if (t.Equals(typeof(Card)) || t.IsAbstract)
                    continue;

                ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
                var ciEx = Expression.New(ci);
                var lambda = Expression.Lambda<Func<Card>>(ciEx);
                var factory = lambda.Compile();
                var card = factory();
                _cards.Add(card.Code, new CardMetadata(card, factory));

                Card c = factory();

                Setmeta meta;
                if (!_sets.TryGetValue(c.Set, out meta))
                {
                    _sets.Add(c.Set, meta = new Setmeta());
                }
                meta.Cards.Add(c);

                if (c.CanBeSupply)
                    meta.Supplies.Add(c);
            }
        }
    }
}
