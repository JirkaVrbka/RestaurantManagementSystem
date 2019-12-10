using System;
using System.Collections.Generic;
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
    public class CompanyServiceTests
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            Container.Install(new TestInstaller());
        }
        [TestMethod]
        public async Task CompanyServiceCreate()
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

        [TestMethod]
        public async Task CompanyServiceFind()
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

        [TestMethod]
        public async Task CompanyServiceGetEmployees()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            CompanyService companyService = Container.Resolve<CompanyService>();
            EmployeeService employeeService = Container.Resolve<EmployeeService>();

            CompanyDto actualCompanyDtoByIco;
            var originalCompanyDto = new CompanyDto
            {
                Ico = 87654321,
                Name = "Kitty",
                JoinDate = DateTime.Now

            };
            CompanyWithEmployeesDto companyWithEmployees;

            using (unitOfWorkProvider.Create())
            {
                companyService.Create(originalCompanyDto);

                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();

                actualCompanyDtoByIco = await companyService.GetByIco(originalCompanyDto.Ico);

                await employeeService.RegisterEmployeeAsync(new EmployeeDto
                {
                    CompanyId = actualCompanyDtoByIco.Id,
                    Email = "an@email.com",
                    FirstName = "name",
                    LastName = "Surmane",
                    PasswordHash = "1111"
                });

                await employeeService.RegisterEmployeeAsync(new EmployeeDto
                {
                    CompanyId = actualCompanyDtoByIco.Id,
                    Email = "asn@email.com",
                    FirstName = "ndame",
                    LastName = "Surmane",
                    PasswordHash = "1111"
                });

                await employeeService.RegisterEmployeeAsync(new EmployeeDto
                {
                    CompanyId = actualCompanyDtoByIco.Id,
                    Email = "asn@email.com",
                    FirstName = "name",
                    LastName = "Surdmane",
                    PasswordHash = "1111"
                });


                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();

                companyWithEmployees = await companyService.GetAsyncWithEmployees(actualCompanyDtoByIco.Id);

            }

            Assert.IsNotNull(companyWithEmployees);
            Assert.AreEqual(3, companyWithEmployees.Employees.Count);
        }
    }
}
