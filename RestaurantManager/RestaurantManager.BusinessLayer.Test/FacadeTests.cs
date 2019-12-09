using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.BusinessLayer.Test.Config;
using RestaurantManager.DAL;

namespace RestaurantManager.BusinessLayer.Test
{
    [TestClass]
    public class FacadeTests
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            Container.Install(new TestInstaller());
        }
    }
}
