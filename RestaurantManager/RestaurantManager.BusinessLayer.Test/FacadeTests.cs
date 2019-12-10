using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;
using RestaurantManager.BusinessLayer.Test.Config;
using RestaurantManager.DAL;
using RestaurantManager.Utils.EntityEnums;

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

        [TestMethod]
        public async Task CreateCompanyAndOwner()
        {
            CompanyFacade companyFacade = Container.Resolve<CompanyFacade>();
            EmployeeFacade employeeFacade = Container.Resolve<EmployeeFacade>();

            NewCustomerDto customer = new NewCustomerDto
            {
                Name = "CompanyName",
                Ico = 13246578,
                Email = "muj@email.com",
                
                FirstName = "jirka",
                LastName = "Novak",
                Password = "123456789"
            };

            await companyFacade.RegisterCompanyWithOwner(customer);
            var company = await companyFacade.GetAsyncByIco(customer.Ico);
            var owner = await employeeFacade.GetAsyncByEmail(customer.Email);

            Assert.AreEqual(company.Id, owner.CompanyId);
            Assert.AreEqual(Role.Owner, owner.Role);

        }
    }
}
