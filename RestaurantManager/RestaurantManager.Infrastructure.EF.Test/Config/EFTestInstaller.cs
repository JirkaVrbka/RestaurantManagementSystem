using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RestaurantManager.DAL;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.EF.UnitOfWork;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.Infrastructure.EF.Test.Config
{
    public class EFTestInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "RestaurantManagerEFTest";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EFUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EFRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EFQuery<>))
                    .LifestyleTransient()
            );
        }

        private static DbContext InitializeDatabase()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            RestaurantManagerDbContext context = new RestaurantManagerDbContext(TestDbConnectionString);

            var company = new Company
            {
                Id = 0,
                Ico = 12345678,
                Name = "Panda",
                Employees = new List<Employee>(),
                JoinDate = DateTime.Now,
                MenuItems = new List<MenuItem>(),
                Orders = new List<Order>(),
                Payments = new List<Payment>(),
                Stock = new List<Stock>()
            };

            context.Companies.Add(company);
            context.SaveChanges();
            return context;
        }
    }
}
