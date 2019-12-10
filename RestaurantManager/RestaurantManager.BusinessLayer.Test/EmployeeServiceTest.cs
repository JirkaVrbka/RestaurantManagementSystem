using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.BusinessLayer.Test.Config;
using RestaurantManager.DAL;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.Test
{
    [TestClass]
    public class EmployeeServiceTest
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            Container.Install(new TestInstaller());
        }
        [TestMethod]
        public async Task EmployeeFindByEmail()
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

            var employee = new EmployeeDto()
            {
                Email = "muj@email.com",
                FirstName = "ja",
                LastName = "ty",
                PasswordHash = "ahoj",
                Role = Role.Owner,
            };

            EmployeeDto actualEmployee;

            using (unitOfWorkProvider.Create())
            {
                companyService.Create(originalCompanyDto);

                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();
                
                employee.CompanyId = (await companyService.GetByIco(originalCompanyDto.Ico)).Id;

                await employeeService.RegisterEmployeeAsync(employee);
                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();

                actualEmployee = await employeeService.GetEmployeeByEmail(employee.Email);
            }

            Assert.IsNotNull(actualEmployee);
            Assert.AreEqual(employee.Email, actualEmployee.Email);
            Assert.AreEqual(employee.CompanyId, actualEmployee.CompanyId);
        }
    }
}
