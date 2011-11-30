using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Interfaces;
using Dominion.Constants;
using Dominion.Model;

namespace Dominion.ServiceModel
{
    public class CardInfo
    {
        public string Name { get; set; }
        public CardCode Code { get; set; }
        public int Cost { get; set; }
        public int VictoryPoints { get; set; }
        public int TreasureValue { get; set; }
        public Uri ImageSource { get; set; }
        public CardSet Set { get; set; }
        public CardType Type { get; set; }
        public bool IsVictory { get; set; }
        public bool IsAction { get; set; }
        public bool IsTreasure { get; set; }
        public bool IsCurse { get; set; }
    }
}
