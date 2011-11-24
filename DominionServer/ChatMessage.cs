using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion
{
    public class ChatMessage
    {
        public int FromPlayerId { get; set; }
        public int ToPlayerId { get; set; }
        public string Message { get; set; }
    }
}
