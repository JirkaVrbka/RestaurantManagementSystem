using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL.Models;
using RestaurantManager.DAL.Tests.Config;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EntityFramework;
using RestaurantManager.Infrastructure.EntityFramework.UnitOfWork;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.DAL.Tests
{
    [TestClass]
    public class CompanyUnitTests : UnitTestsBase
    {
        [TestMethod]
        public void CreateCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(provider);

            var dbCompany = repository.GetAsync(defaultCompany.Id).Result;
            
            Assert.AreEqual(defaultCompany.Name, dbCompany.Name);
            Assert.AreEqual(defaultCompany.Ico, dbCompany.Ico);
            Assert.AreEqual(defaultCompany.JoinDate.ToString(), dbCompany.JoinDate.ToString());
        }

        [TestMethod]
        public async Task UpdateCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(provider);

            defaultCompany.Name = "Karlova tovarna na cokoladu";
            repository.Update(defaultCompany);
            await unitOfWork.Commit();
            var dbCompany = repository.GetAsync(defaultCompany.Id).Result;

            Assert.AreEqual(defaultCompany.Name, dbCompany.Name);
            Assert.AreEqual(defaultCompany.Ico, dbCompany.Ico);
            Assert.AreEqual(defaultCompany.JoinDate.ToString(), dbCompany.JoinDate.ToString());
        }

        [TestMethod]
        public async Task DeleteCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(provider);

            repository.Delete(defaultCompany.Id);
            await unitOfWork.Commit();
            var dbCompany = await repository.GetAsync(defaultCompany.Id);

            Assert.AreEqual(null, dbCompany);
        }
    }
}
