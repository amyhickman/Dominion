using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Dominion.Util
{
    public class MyValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            
        }
    }
}