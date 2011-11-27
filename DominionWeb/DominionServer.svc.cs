using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Timers;
using Dominion.Model;
using System.Security.Principal;

namespace Dominion
{
    public class DominionServer : IDominion//, IChatServer, IGameServer
    {
        private const string CACHE_PROFILE_KEY = "userProfile_";

        private IIdentity GetIdentity()
        {
            var sec = OperationContext.Current.ServiceSecurityContext;
            return sec.PrimaryIdentity;
        }


        public void Login()
        {

        }

        public void Logout()
        {
        }

        //public void BuyCard(CardCode id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void TrashCard(int cardId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void PlayCard(int cardId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RevealCard(int cardId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DrawCard()
        //{
        //    throw new NotImplementedException();
        //}

        //public void DiscardCard(int cardId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ShuffleDeck(int deckId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void MoveCard(int cardId, int destinationId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetAsideCard(int cardId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Resign()
        //{
        //    throw new NotImplementedException();
        //}

        //public void JoinGame(int gameId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void CreateGame()
        //{
        //    throw new NotImplementedException();
        //}



        //public void EndTurn()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
