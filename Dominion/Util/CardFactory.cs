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
        private class CardMeta
        {
            public Func<Card> Factory { get; set; }
            public Card Meta { get; set; }

            public CardMeta(Card c, Func<Card> fact) { Factory = fact; Meta = c; }
        }

        private static readonly Dictionary<CardCode, CardMeta> _cards = new Dictionary<CardCode, CardMeta>();
        private static readonly Dictionary<CardSet, List<CardCode>> _cardsBySet = new Dictionary<CardSet, List<CardCode>>();
        private static readonly List<Card> _allCards = new List<Card>();
        private static readonly List<CardCode> _eligibleSupplyCards;

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
                _cards.Add(card.Code, new CardMeta(card, factory));

                Card c = factory();
                _allCards.Add(c);

                List<CardCode> codes;
                if (!_cardsBySet.TryGetValue(c.Set, out codes))
                {
                    _cardsBySet.Add(c.Set, codes = new List<CardCode>());
                }
                codes.Add(c.Code);
            }

            _eligibleSupplyCards = _allCards.Where(c => c.CanBeSupply).Select(c => c.Code).ToList();
        }

        private int _nextId = 0;

        public CardFactory(Game g)
        {
            Game = g;
        }

        public Card CreateCard(CardCode id)
        {
            Card c = _cards[id].Factory();
            c.Game = Game;
            c.Id = _nextId++;
            return c;
        }

        public IList<Card> CreateCards(CardCode id, int quantity)
        {
            List<Card> retval = new List<Card>();
            while (quantity > 0)
            {
                retval.Add(CreateCard(id));
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

        public static IList<CardCode> GetAllKnownCardCodes()
        {
            return _allCards.Select(c => c.Code).ToList();
        }

        public static IList<Card> GetAllKnownCards()
        {
            return new List<Card>(_allCards);
        }

        public static IList<CardCode> GetEligibleSupplyCardCodes()
        {
            return new List<CardCode>(_eligibleSupplyCards);
        }

        public static Card GetCardMeta(CardCode code)
        {
            return _cards[code].Meta;
        }
    }
}