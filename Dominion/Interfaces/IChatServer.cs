using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dominion.Model;

namespace Dominion
{
    public interface IChatServer
    {
        void MutePlayer(int playerId);
        void UnmutePlayer(int playerId);

        List<Room> GetRoomList();
        Room CreateRoom(string name);
        bool JoinRoom(int roomId);

    }
}
