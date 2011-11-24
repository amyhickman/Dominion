using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dominion.Model;

namespace Dominion
{
    [ServiceContract(CallbackContract=typeof(IChatMessageCallback))]
    public interface IChatServer
    {
        void SendRoomMessage(int roomId, ChatMessage message);
        void SendGameMessag(int gameId, ChatMessage message);
        void SendPrivateMessage(int playerId, ChatMessage message);

        void MutePlayer(int playerId);
        void UnmutePlayer(int playerId);

        List<Room> GetRoomList();
        Room CreateRoom(string name);
        bool JoinRoom(int roomId);

    }
}
