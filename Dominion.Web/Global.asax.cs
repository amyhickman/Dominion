using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace Dominion.Web
{
    public class Hello
    {
        public string Name { get; set; }
    }
    public class HelloResponse
    {
        public string Name { get; set; }
    }
    public class HelloService : IService<Hello>
    {
        public object Execute(Hello request)
        {
            return new HelloResponse() { Name = request.Name + " is a super hero" };
        }
    }

    public class HelloAppHost : AppHostBase
    {
        //Tell Service Stack the name of your application and where to find your web services
        public HelloAppHost()
            : base("Hello Web Services", typeof(HelloService).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            Routes.Add<Hello>("/hello")
                .Add<Hello>("/hello/{Name}");
        }


    }

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("servicestack/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);


            var host = new HelloAppHost();
            //host.Config.EnableFeatures = ServiceStack.ServiceHost.Feature.
            host.Init();
        }
    }
}