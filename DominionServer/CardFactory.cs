using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;
using Dominion.Model;

namespace Dominion
{
    public class CardFactory
    {
        private static Dictionary<CardCode, Func<Card>> _cards = new Dictionary<CardCode, Func<Card>>();

        static CardFactory()
        {
            Assembly assembly = typeof(Card).Assembly;
            foreach (var t in assembly.GetTypes().Where(x => x.IsAssignableFrom(typeof(Card))))
            {
                if (t.Equals(typeof(Card)) || t.IsAbstract)
                    continue;

                ConstructorInfo ci = t.GetConstructor(Type.EmptyTypes);
                var ciEx = Expression.New(ci);
                var lambda = Expression.Lambda<Func<Card>>(ciEx);
                var factory = lambda.Compile();
                var card = factory();
                _cards.Add(card.CardCode, factory);
            }
        }

        private int _nextId = 0;

        public Card GetCard(CardCode id)
        {
            Card c = _cards[id]();
            c.CardId = _nextId++;
            return c;
        }
    }
}