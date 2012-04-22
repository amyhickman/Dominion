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
        private static readonly Dictionary<CardSet, List<CardCode>> _cardsInSet = new Dictionary<CardSet, List<CardCode>>();
        private static readonly Dictionary<CardSet, List<CardCode>> _suppliesInSet = new Dictionary<CardSet, List<CardCode>>();
        private static readonly Dictionary<CardCode, Func<Card>> _cards = new Dictionary<CardCode, Func<Card>>();

        public static IList<CardCode> GetSuppliesInSet(CardSet set)
        {
            if (_suppliesInSet.ContainsKey(set))
                return new List<CardCode>(_suppliesInSet[set]);

            return new List<CardCode>();
        }

        public static Card CreateCard(CardCode code)
        {
            return _cards[code]();
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
                _cards.Add(card.Code, factory);

                List<CardCode> codes;

                if (!_cardsInSet.TryGetValue(card.Set, out codes))
                {
                    _cardsInSet.Add(card.Set, codes = new List<CardCode>());
                }
                codes.Add(card.Code);

                if (card.CanBeSupply)
                {
                    if (!_suppliesInSet.TryGetValue(card.Set, out codes))
                    {
                        _suppliesInSet.Add(card.Set, codes = new List<CardCode>());
                    }
                    codes.Add(card.Code);
                }
            }
        }
    }
}
