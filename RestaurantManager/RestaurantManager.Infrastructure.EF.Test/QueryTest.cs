using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL;
using RestaurantManager.Infrastructure.EF.Test.Config;

namespace RestaurantManager.Infrastructure.EF.Test
{
    [TestClass]
    public class QueryTest
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [TestInitialize]
        public void Init()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            Container.Install(new EFTestInstaller());
        }

    }
}
