using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using RestaurantManager.BusinessLayer.Config;
using WebApi.App_Start.Windsor;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void BootstrapContainer()
        {
            container.Install(new WebApiInstaller());
            container.Install(new BusinessLayerInstaller());

            GlobalConfiguration.Configuration.Services
                .Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }
    }
}
