using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Castle.Windsor;
using RestaurantManager.BusinessLayer.Config;
using RestaurantManager.BusinessLayer.Facades;
using Web.App_Start.Windsor;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            // configure DI            
            Container.Install(new BusinessLayerInstaller());
            Container.Install(new PresentationLayerInstaller());

            // set controller factory
            var controllerFactory = new WindsorControllerFactory(Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            if (Container.Kernel.HasComponent(typeof(CompanyFacade)))
            {
                Console.WriteLine("yes");
                // resolve service with container.Resolve<IMyService>()
                // then do cool stuff
            }
            else
            {
                Console.WriteLine("No");
            }
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }

        public override void Dispose()
        {
            Container.Dispose();
            base.Dispose();
        }
    }
}
