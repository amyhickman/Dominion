using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Exceptions
{
    public class YouCantDoThatException : Exception
    {
        public string Reason { get; set; }

        public YouCantDoThatException(string reason)
        {
            Reason = reason;
        }
    }
}
