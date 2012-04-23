using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Util;
using Dominion.Constants;

namespace Dominion.Model
{
    public class GameFactory
    {
        private static readonly CardCode[] _defaultSupplyCodes = new CardCode[] 
        {
            CardCode.Duchy, CardCode.Province, CardCode.Estate,
            CardCode.Gold, CardCode.Silver, CardCode.Copper
        };

        private IList<Player> Players { get; set; }
        public IList<CardCode> DesiredSupplies { get; private set; }
        public IList<CardCode> UndesiredSupplies { get; private set; }
        public IList<CardSet> DesiredSets { get; private set; }

        public GameFactory()
        {
            Players = new List<Player>();
            DesiredSupplies = new List<CardCode>();
            UndesiredSupplies = new List<CardCode>();
            DesiredSets = new List<CardSet>();
        }

        public void AddPlayer(Player p)
        {
            if (p == null)
                throw new ArgumentNullException("p");
            if (p.Game != null)
                throw new InvalidOperationException("This player is already associated with a game");

            Players.Add(p);
        }

        public IList<CardCode> GenerateRandomSupplies()
        {
            var effectiveDesiredSets = new List<CardSet>(DesiredSets.Count > 0 ? DesiredSets : (CardSet[])Enum.GetValues(typeof(CardSet)));
            
            var candidates = new List<CardCode>(effectiveDesiredSets
                .SelectMany(set => CardDirectory.GetSuppliesInSet(set))
                .Except(UndesiredSupplies))
                .Shuffle()
                .Take(10);
            
            return DesiredSupplies.Concat(candidates).Take(10).ToList();
        }

        private IList<CardCode> GetRequiredAdditionalSupplies(IList<CardCode> supplies)
        {
            return supplies.SelectMany(code => CardDirectory.CreateCard(code).RequiresInSupply)
                .Distinct()
                .ToList();
        }

        private IList<CardCode> GetTotalSupplies(IList<CardCode> desiredSupplies)
        {
            return desiredSupplies
                .Concat(GetRequiredAdditionalSupplies(desiredSupplies))
                .Concat(_defaultSupplyCodes)
                .Distinct()
                .ToList();
        }

        private void SetupInitialHands()
        {
            foreach (var p in Players)
            {
                p.Deck.AddRange(CardDirectory.CreateCards(CardCode.Copper, 7));
                p.Deck.AddRange(CardDirectory.CreateCards(CardCode.Estate, 3));
                p.Deck.Shuffle();
                p.Hand.AddRange(p.Deck.Draw(5));
            }
        }

        public Game CreateGame()
        {
            return CreateGame(GenerateRandomSupplies());
        }

        public Game CreateGame(IList<CardCode> desiredSupplies)
        {
            if (Players.Count < 2)
                throw new InvalidOperationException("Not enough players");


            SetupInitialHands();
            var g = new Game(Players, GetTotalSupplies(desiredSupplies));

            foreach (var p in Players)
                p.Game = g;

            return g;
        }
    }
}
