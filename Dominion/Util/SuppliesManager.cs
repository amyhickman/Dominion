using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;
using log4net;
using Dominion.Constants;

namespace Dominion.Util
{
    public class SuppliesManager : IEnumerable<CardCode>
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(SuppliesManager));

        private readonly Dictionary<CardCode, CardContainer> _supplies = new Dictionary<CardCode, CardContainer>();

        public Game Game { get; private set; }

        public SuppliesManager(Game game, IList<CardCode> supplies)
        {
            Game = game;
            foreach (var s in supplies)
                AddSupply(s);
        }

        private CardContainer CreateSupplyPile(CardCode code)
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
                Card card = CardDirectory.CreateCard(code);
                pile.Add(card);
            }

            return pile;
        }

        public void AddSupply(CardCode code)
        {
            if (!CardDirectory.CreateCard(code).CanBeSupply)
                throw new InvalidOperationException("Card " + code.ToString() + " cannot be used as a supply.");

            if (_supplies.ContainsKey(code))
                throw new InvalidOperationException("Supply already added.");

            var pile = CreateSupplyPile(code);
            _supplies.Add(code, pile);
        }

        public CardContainer this[CardCode code]
        {
            get { return _supplies[code]; }
        }

        public bool HasSupply(CardCode code) { return _supplies.ContainsKey(code); }

        public IList<CardCode> GetSupplyCodes() { return new List<CardCode>(_supplies.Keys); }

        public IEnumerator<CardCode> GetEnumerator()
        {
            return _supplies.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _supplies.Keys.GetEnumerator();
        }
    }
}
