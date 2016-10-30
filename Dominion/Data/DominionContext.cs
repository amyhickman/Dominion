using System;
using System.Data.Entity;

namespace Dominion.Data
{
    public class DominionContext : DbContext
    {
        public static Func<DominionContext> Create = () => new DominionContext();
 
        public DominionContext()
            : base("DefaultConnection")
        {
            
        }
    }
}
