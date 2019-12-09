using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RestaurantManager.BusinessLayer.Config;

namespace Web.App_Start.Windsor
{
    public class PresentationLayerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IController>()
                    .LifestyleTransient()
            );
        }
    }
}