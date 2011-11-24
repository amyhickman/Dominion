using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion
{
    public interface IChatMessageCallback
    {
        void GameChatReceived(int gameId, ChatMessage message);
        void RoomChatReceived(int roomId, ChatMessage message);
        void PrivateMessageReceived(ChatMessage message);
    }
}
