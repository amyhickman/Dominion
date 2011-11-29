using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Util;
using Dominion.Constants;

namespace Dominion.Model
{
    public class GameSetup
    {
        private static readonly CardCode[] _defaultSupplyCodes = new CardCode[] 
        {
            CardCode.Duchy, CardCode.Province, CardCode.Estate,
            CardCode.Gold, CardCode.Silver, CardCode.Copper
        };

        public IList<Player> Players { get; private set; }
        public IList<Player> Watchers { get; private set; }
        public IList<CardCode> DesiredSupplies { get; private set; }
        public IList<CardCode> UndesiredSupplies { get; private set; }
        public IList<CardSet> DesiredSets { get; private set; }

        public GameSetup()
        {
            Players = new List<Player>();
            DesiredSupplies = new List<CardCode>();
            UndesiredSupplies = new List<CardCode>();
            DesiredSets = new List<CardSet>();
            Watchers = new List<Player>();
        }

        public IList<CardCode> GenerateRandomSupplies()
        {
            var effectiveDesiredSets = new List<CardSet>(DesiredSets.Count > 0 ? DesiredSets : (CardSet[])Enum.GetValues(typeof(CardSet)));
            
            var candidates = new List<CardCode>(effectiveDesiredSets
                .SelectMany(set => CardDirectory.GetSuppliesInSet(set).Select(meta=> meta.Code))
                .Except(UndesiredSupplies))
                .Shuffle()
                .Take(10);
            
            return DesiredSupplies.Concat(candidates).Take(10).ToList();
        }

        private IList<CardCode> GetRequiredAdditionalSupplies(IList<CardCode> supplies)
        {
            return supplies.SelectMany(code => CardDirectory.GetCardMeta(code).RequiresInSupply)
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

        public Game CreateGame()
        {
            return CreateGame(GenerateRandomSupplies());
        }

        public Game CreateGame(IList<CardCode> desiredSupplies)
        {
            if (Players.Count < 2)
                throw new InvalidOperationException("Not enough players");

            return new Game(Players, GetTotalSupplies(desiredSupplies));
        }
    }
}
