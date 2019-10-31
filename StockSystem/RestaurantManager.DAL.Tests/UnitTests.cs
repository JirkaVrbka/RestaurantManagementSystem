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
    public class UnitTests
    {
        private EntityFrameworkUnitOfWorkProvider provider;
        private IUnitOfWork unitOfWork;
        private Company defaultCompany;


        [TestInitialize]
        public void DbInitializer()
        {
            provider = new EntityFrameworkUnitOfWorkProvider(TestInitializer.InitializeDbContext);
            unitOfWork = provider.Create();
            defaultCompany = new Company
            {
                Ico = 1234,
                JoinDate = DateTime.Now,
                Name = "Alfonz"
            };
            CreateDefaultCompanyInDb();
        }

        [TestCleanup]
        public void DbCleaner()
        {
            provider.Dispose();
            unitOfWork.Dispose();
        }

        [TestMethod]
        public async Task CreateCompanyTest()
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

        private void CreateDefaultCompanyInDb()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(provider);
            repository.Create(defaultCompany);
            unitOfWork.Commit().Wait();
        }
    }
}
