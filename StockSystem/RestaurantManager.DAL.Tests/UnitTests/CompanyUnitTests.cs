using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.EntityFramework;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Tests.UnitTests
{
    [TestClass]
    public class CompanyUnitTests : UnitTestsBase
    {
        [TestMethod]
        public void CreateCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(Provider);

            var dbCompany = repository.GetAsync(DefaultCompany.Id).Result;
            
            Assert.AreEqual(DefaultCompany.Name, dbCompany.Name);
            Assert.AreEqual(DefaultCompany.Ico, dbCompany.Ico);
            Assert.AreEqual(DefaultCompany.JoinDate.ToString(), dbCompany.JoinDate.ToString());
        }

        [TestMethod]
        public async Task UpdateCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(Provider);

            DefaultCompany.Name = "Karlova tovarna na cokoladu";
            repository.Update(DefaultCompany);
            await UnitOfWork.Commit();
            var dbCompany = repository.GetAsync(DefaultCompany.Id).Result;

            Assert.AreEqual(DefaultCompany.Name, dbCompany.Name);
            Assert.AreEqual(DefaultCompany.Ico, dbCompany.Ico);
            Assert.AreEqual(DefaultCompany.JoinDate.ToString(), dbCompany.JoinDate.ToString());
        }

        [TestMethod]
        public async Task DeleteCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(Provider);

            repository.Delete(DefaultCompany.Id);
            await UnitOfWork.Commit();
            var dbCompany = await repository.GetAsync(DefaultCompany.Id);

            Assert.AreEqual(null, dbCompany);
        }
    }
}
