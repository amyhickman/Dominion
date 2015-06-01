using Autofac;
using Nancy.Bootstrappers.Autofac;

namespace Dominion.Startup
{
    public class NancyBootstrapper : AutofacNancyBootstrapper
    {
        private readonly IContainer _container;

        public NancyBootstrapper(IContainer container)
        {
            _container = container;
        }

        protected override ILifetimeScope GetApplicationContainer()
        {
            return _container;
        }
    }
}
