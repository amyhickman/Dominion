using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Model;
using Dominion.OldModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dominion.Data
{
    public class DominionContext : IdentityDbContext<ApplicationUser>
    {
        public static Func<DominionContext> Create = () => new DominionContext();
 
        public DominionContext()
            : base("DefaultConnection")
        {
            
        }
    }
}
