using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dominion.Model;

namespace Dominion
{
    [ServiceContract]
    public interface IDominion
    {
        [OperationContract]
        void Login();

        void Logout();


        //void JoinGame(int gameId);
        //void CreateGame();

        //[OperationContract(IsInitiating=false)]
        //void Chat(int roomId, string text);
    }
}
