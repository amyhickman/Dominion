using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;
using Dominion.Constants;

namespace Dominion.Interfaces
{
    public interface ICardMetadata
    {
        string Name { get; }
        CardCode Code { get; }
        int Cost { get; }
        int VictoryPoints { get; }
        int TreasureValue { get; }
        Uri ImageSource { get; }
        CardSet Set { get; }
        CardType Type { get; }
        IList<CardCode> RequiresInSupply { get; }

        bool IsVictory { get; }
        bool IsAction { get; }
        bool IsTreasure { get; }
        bool IsCurse { get; }
        bool CanBeSupply { get; }
    }
}
