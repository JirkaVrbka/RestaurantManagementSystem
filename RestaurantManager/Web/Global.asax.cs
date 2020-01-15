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
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.BusinessLayer.Facades;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
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

            // TODO remove to release
            /*foreach (var handler in Container.Kernel.GetAssignableHandlers(typeof(object)))
            {
                System.Diagnostics.Debug.WriteLine("{0} {1}",
                    handler.ComponentModel.Services,
                    handler.ComponentModel.Implementation);
            }

            IQuery<Order> o = Container.Resolve<IQuery<Order>>();

            QueryObjectBase<OrderDto, Order, OrderFilterDto, IQuery<Order>> query2 =
                Container.Resolve<QueryObjectBase<OrderDto, Order, OrderFilterDto, IQuery<Order>>>();

            QueryObjectBase<OrderWithFullDependencyDto, Order, OrderFilterDto, IQuery<Order>> query =
                Container.Resolve<QueryObjectBase<OrderWithFullDependencyDto, Order, OrderFilterDto, IQuery<Order>>>();*/
            //CompanyFacade companyFacade = Container.Resolve<CompanyFacade>();
            Console.WriteLine("OK");
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }
    }
}
