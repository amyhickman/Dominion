using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;
using log4net;
using Dominion.Constants;

namespace Dominion.Util
{
    public class SuppliesManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(SuppliesManager));

        private readonly Dictionary<CardCode, CardContainer> _supplies = new Dictionary<CardCode, CardContainer>();

        public Game Game { get; private set; }
        private CardManager _cardFactory;

        public SuppliesManager(Game game, CardManager factory, IList<CardCode> supplies)
        {
            Game = game;
            _cardFactory = factory;
        }

        private void CreateSupplyPile(CardCode code)
        {
            CardContainer pile = new CardContainer(null);
            int quantity = 10;

            switch (code)
            {
                case CardCode.Estate:
                case CardCode.Duchy:
                case CardCode.Province:
                    quantity = (Game.GetPlayers().Count == 2) ? 8 : 12;
                    break;
            }

            _log.Info(String.Format("Generating supply pile for card: {0}, Quantity: {1}", code, quantity));

            for (int i = 0; i < quantity; i++)
            {
                Card card = _cardFactory.CreateCard(code);
                pile.Add(card);
            }

            _supplies.Add(code, pile);
        }

        public void AddSupply(CardCode code)
        {
            if (!CardDirectory.GetCardMeta(code).CanBeSupply)
                throw new InvalidOperationException("Card " + code.ToString() + " cannot be used as a supply.");

            if (_supplies.ContainsKey(code))
                throw new InvalidOperationException("Supply already added.");

            CreateSupplyPile(code);
        }

        public CardContainer this[CardCode code]
        {
            get { return _supplies[code]; }
        }

        public bool HasSupply(CardCode code) { return _supplies.ContainsKey(code); }

        public IList<CardCode> GetSupplyCodes() { return new List<CardCode>(_supplies.Keys); }
    }
}
