using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.BusinessLayer.Test.Config;
using RestaurantManager.DAL;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.BusinessLayer.Test
{
    [TestClass]
    public class ServicesTests
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            Container.Install(new TestInstaller());
        }
        [TestMethod]
        public async Task CompanyService()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            CompanyService companyService = Container.Resolve<CompanyService>();

            CompanyDto actualCompanyDtoByIco;
            var originalCompanyDto = new CompanyDto
            {
                Ico = 87654321,
                Name = "Kitty",
                JoinDate = DateTime.Now

            };
            using (unitOfWorkProvider.Create())
            {
                companyService.Create(originalCompanyDto);

                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();

                actualCompanyDtoByIco = await companyService.GetByIco(originalCompanyDto.Ico);
                
            }

            Assert.IsNotNull(actualCompanyDtoByIco);
            Assert.AreEqual(originalCompanyDto.Name, actualCompanyDtoByIco.Name);
            Assert.AreEqual(originalCompanyDto.Ico, actualCompanyDtoByIco.Ico);
        }
    }
}
