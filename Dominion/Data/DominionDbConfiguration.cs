using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion.Data
{
    public class DominionDbConfiguration : DbConfiguration
    {
        public DominionDbConfiguration() 
        {
            SetHistoryContext(SqlProviderServices.ProviderInvariantName, (connection, s) => new DominionHistoryContext(connection, s));   
        }
    }
}
