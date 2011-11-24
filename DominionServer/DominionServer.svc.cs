using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Caching;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Timers;
using Dominion.Model;
using System.Security.Principal;

namespace Dominion
{
    public class DominionServer : IDominion//, IChatServer, IGameServer
    {
        private const string CACHE_PROFILE_KEY = "userProfile_";


        private MemoryCache _cache = new MemoryCache("gamesCache");




        private IIdentity GetIdentity()
        {
            var sec = OperationContext.Current.ServiceSecurityContext;
            return sec.PrimaryIdentity;
        }

        private UserProfile GetUserProfile()
        {
            var x = GetIdentity();
            var profile = _cache.Get(CACHE_PROFILE_KEY + x.Name) as UserProfile;
            if (profile == null)
            {
                _cache.Add(CACHE_PROFILE_KEY + x.Name, profile = new UserProfile(x), new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromHours(2) });
            }
            return profile;
        }

        public void Login()
        {

        }

        public void Logout()
        {
            _cache.Remove(CACHE_PROFILE_KEY + GetIdentity().Name);
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
