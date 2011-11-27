using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dominion.Model;

namespace Dominion
{
    public interface IGameServer
    {
        #region Card operations
        void BuyCard(CardCode id);
        void BuyCard(string name);

        void DiscardCard(int cardId); 
        void TrashCard(int cardId);
        void PlayCard(int cardId);
        void RevealCard(int cardId);
        void DrawCard();

        #endregion


        void ShuffleDeck(int deckId);
        void MoveCard(int cardId, int destinationId);
        void SetAsideCard(int cardId);
        void EndTurn();

        void Resign();
    }
}
