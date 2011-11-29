using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Dominion.ServiceModel
{
    public abstract class DominionGetServiceBase<T> : ServiceBase<T>, IRestGetService<T>
    {
        protected override object Run(T request)
        {
            return Get(request);
        }

        public abstract object Get(T request);
    }
}
