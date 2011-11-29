using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;
using Dominion.Model;
using Dominion.Interfaces;
using log4net;
using Dominion.Constants;

namespace Dominion.Util
{
    public class CardManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(CardManager));
        private readonly Dictionary<Guid, Card> _cardsById = new Dictionary<Guid, Card>();

        public Game Game { get; private set; }

        public CardManager(Game g)
        {
            Game = g;
        }

        public Card CreateCard(CardCode code)
        {
            Card c = CardDirectory.CreateCard(code);
            _cardsById.Add(c.Id, c);
            c.Game = Game;
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

        public Card this[Guid id]
        {
            get { return _cardsById[id]; }
        }
    }
}