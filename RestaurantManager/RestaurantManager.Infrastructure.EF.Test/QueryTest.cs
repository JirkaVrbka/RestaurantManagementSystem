using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.EF.Test.Config;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.Infrastructure.EF.Test
{/*
    [TestClass]
    public class QueryTest
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Container.Install(new EFTestInstaller());
        }

        [TestMethod]
        public async Task SimpleWhereWithResult()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            QueryResult<MenuItem> actualQueryResult;
            var menuItemQuery = Container.Resolve<IQuery<MenuItem>>();

            var predicate = new SimplePredicate(nameof(MenuItem.SellPrice), ValueComparingOperator.GreaterThan, 10);

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await menuItemQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(2 , actualQueryResult.Items.Count);
            Assert.IsTrue(actualQueryResult.Items[0].SellPrice > 10);
            Assert.IsTrue(actualQueryResult.Items[1].SellPrice > 10);
        }

        [TestMethod]
        public async Task SimpleWhereWithNoResult()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            QueryResult<MenuItem> actualQueryResult;
            var menuItemQuery = Container.Resolve<IQuery<MenuItem>>();

            var predicate = new SimplePredicate(nameof(MenuItem.SellPrice), ValueComparingOperator.LessThan, 0);

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await menuItemQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(0, actualQueryResult.Items.Count);
        }

        [TestMethod]
        public async Task SimpleWhereWithNoResultViaString()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            QueryResult<MenuItem> actualQueryResult;
            var menuItemQuery = Container.Resolve<IQuery<MenuItem>>();

            var predicate = new SimplePredicate(nameof(MenuItem.Name), ValueComparingOperator.Equal, "Vecere");

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await menuItemQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(1, actualQueryResult.Items.Count);
        }

        [TestMethod]
        public async Task ComplexWhereWithResult()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            QueryResult<MenuItem> actualQueryResult;
            var menuItemQuery = Container.Resolve<IQuery<MenuItem>>();

            var predicate = new CompositePredicate(new List<IPredicate>()
            {
                new SimplePredicate(nameof(MenuItem.SellPrice), ValueComparingOperator.GreaterThan, 10),
                new SimplePredicate(nameof(MenuItem.SellPrice), ValueComparingOperator.LessThan, 30),
                new SimplePredicate(nameof(MenuItem.CompanyId), ValueComparingOperator.Equal, 1)
            }, LogicalOperator.AND);

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await menuItemQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(1, actualQueryResult.Items.Count);
            Assert.IsTrue(actualQueryResult.Items[0].SellPrice == 20);
        }

    }*/
}
