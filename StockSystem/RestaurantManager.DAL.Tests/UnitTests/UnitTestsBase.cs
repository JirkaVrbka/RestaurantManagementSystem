using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UnitTestsBase
    {
        protected EntityFrameworkUnitOfWorkProvider provider;
        protected IUnitOfWork unitOfWork;
        protected Company defaultCompany;


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

        private void CreateDefaultCompanyInDb()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(provider);
            repository.Create(defaultCompany);
            unitOfWork.Commit().Wait();
        }


    }
}
