using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.Attributed;
using Autofac.Extras.NLog;
using Autofac.Integration.WebApi;
using Dominion.Constants;
using Dominion.Data;
using Dominion.Model;
using Dominion.OldModel;
using Dominion.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dominion.Startup
{
    public class DependencyRegistrar
    {
        private static readonly Assembly ThisAssembly = typeof(Program).Assembly;

        public void Register(ContainerBuilder builder)
        {
            builder.RegisterModule<NLogModule>();
            builder.RegisterEventing();
            builder.RegisterApiControllers(ThisAssembly);
            builder.RegisterType<Program>();

            builder.RegisterType<DominionContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .As<Card>()
                .AsSelf()
                .Named<Card>(t => t.Name)
                .WithMetadataFrom<CardInfoAttribute>();
        }
    }

    [MetadataAttribute]
    public class CardInfoAttribute : Attribute
    {
        public CardSet Set { get; private set; }

        public CardInfoAttribute(CardSet set)
        {
            Set = set;
        }
    }
}
