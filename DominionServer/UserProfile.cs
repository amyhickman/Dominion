using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Dominion
{
    public class UserProfile
    {
        public IIdentity Identity { get; private set; }

        public UserProfile(IIdentity identity)
        {
            Identity = identity;
        }

    }
}