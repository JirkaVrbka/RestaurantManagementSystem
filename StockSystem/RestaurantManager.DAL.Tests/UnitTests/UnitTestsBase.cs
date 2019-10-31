using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL.Models;
using RestaurantManager.DAL.Tests.Config;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EntityFramework;
using RestaurantManager.Infrastructure.EntityFramework.UnitOfWork;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.DAL.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected EntityFrameworkUnitOfWorkProvider Provider;
        protected IUnitOfWork UnitOfWork;
        protected Company DefaultCompany;


        [TestInitialize]
        public void DbInitializer()
        {
            Provider = new EntityFrameworkUnitOfWorkProvider(TestInitializer.InitializeDbContext);
            UnitOfWork = Provider.Create();
            DefaultCompany = new Company
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
            Provider.Dispose();
            UnitOfWork.Dispose();
        }

        private void CreateDefaultCompanyInDb()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(Provider);
            repository.Create(DefaultCompany);
            UnitOfWork.Commit().Wait();
        }
    }
}
