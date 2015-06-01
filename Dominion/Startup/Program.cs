using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Extras.NLog;
using Autofac.Integration.WebApi;
using Dominion.Data;
using Dominion.Model;
using Dominion.OldModel;
using Dominion.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Nancy;
using Nancy.Owin;
using NLog;
using Owin;
using Topshelf;
using Topshelf.Autofac;


namespace Dominion.Startup
{
    class Program
    {
        private static readonly IContainer Container;
        private static readonly ILogger Logger;
        private static IDisposable _webApplication;

        static Program()
        {
            Container = new Func<IContainer>(() =>
            {
                var builder = new ContainerBuilder();
                var registrar = new DependencyRegistrar();
                registrar.Register(builder);
                return builder.Build();
            })();

            Logger = new LoggerAdapter(LogManager.GetCurrentClassLogger());
        }

        static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(host =>
            {
                host.UseAutofacContainer(Container);
                host.UseNLog();

                host.Service<Program>(service =>
                {
                    service.ConstructUsingAutofacContainer();
                    service.WhenStarted(p => p.Started());
                    service.WhenStopped(p => p.Stopped());
                    service.BeforeStartingService(BeforeStartingService);
                    service.BeforeStoppingService(BeforeStoppingService);
                });

                host.SetDescription("Hosts the Dominion Card Game.");
                host.SetDisplayName("Dominion Server");
                host.SetServiceName("DominionServer");
                host.RunAsNetworkService();
            });

            return (int)exitCode;
        }

        private void Started()
        {
        }

        private void Stopped()
        {
        }

        private static void BeforeStartingService(HostStartContext ctx)
        {
            var opts = new StartOptions("http://localhost:8080");
            Logger.Info("Starting OWIN self-host, listening on: {0}", string.Join(", ", opts.Urls));
            _webApplication = WebApp.Start(opts, OwinStartup);
        }

        private static void BeforeStoppingService(HostStopContext ctx)
        {
            Logger.Info("Stopping OWIN self-host");
            _webApplication.Dispose();
        }

        private static void OwinStartup(IAppBuilder app)
        {
            var http = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(Container)
            };
            http.MapHttpAttributeRoutes();

            UpdateContainerWithOwinRegistrations(app);
            app.UseAutofacMiddleware(Container);
            
            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationMode = AuthenticationMode.Passive,
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    CookieSecure = CookieSecureOption.SameAsRequest,
            //    LoginPath = new PathString("/Account/Login")
            //});
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions());

            app.UseWebApi(http);

            app.UseNancy(options =>
            {
                options.Bootstrapper = new NancyBootstrapper(Container); 
                options.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            });
            
            app.UseErrorPage();
        }

        private static void UpdateContainerWithOwinRegistrations(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.Register(c => c.Resolve<IOwinContext>().Authentication)
                .As<IAuthenticationManager>()
                .InstancePerRequest();

            builder.Register(c => app.GetDataProtectionProvider())
                .As<IDataProtectionProvider>()
                .InstancePerRequest();
            builder.Update(Container);
        }
    }
}
