using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;
using Dominion.Model;

namespace Dominion.Util
{
    public class CardFactory
    {
        private static Dictionary<CardCode, Func<Card>> _cards = new Dictionary<CardCode, Func<Card>>();
        private static Dictionary<CardSet, List<CardCode>> _cardsBySet = new Dictionary<CardSet, List<CardCode>>();
        private static List<Card> _allCards = new List<Card>();

        public Game Game { get; private set; }

        static CardFactory()
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

                Card c = factory();
                _allCards.Add(c);

                List<CardCode> codes;
                if (!_cardsBySet.TryGetValue(c.Set, out codes))
                {
                    _cardsBySet.Add(c.Set, codes = new List<CardCode>());
                }
                codes.Add(c.Code);
            }
        }

        private int _nextId = 0;

        public CardFactory(Game g)
        {
            Game = g;
        }

        public Card GetCard(CardCode id)
        {
            Card c = _cards[id]();
            c.Game = Game;
            c.Id = _nextId++;
            return c;
        }

        public IList<Card> GetCards(CardCode id, int quantity)
        {
            List<Card> retval = new List<Card>();
            while (quantity > 0)
            {
                retval.Add(GetCard(id));
                quantity--;
            }
            return retval;
        }

        public static IList<CardCode> GetCardsInSet(CardSet set)
        {
            if (_cardsBySet.ContainsKey(set))
                return _cardsBySet[set];
            return new List<CardCode>();
        }

        public static IList<CardCode> AllKnownCardCodes
        {
            get { return _cards.Keys.ToList(); }
        }
    }
}