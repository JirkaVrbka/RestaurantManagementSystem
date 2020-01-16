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
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.Infrastructure.EF.Test.Config
{
    public class EFTestInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "RestaurantManagerEFTest";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // TODO this does not drop DB with every new test
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
                Ico = 12345678,
                Name = "Panda",
                Employees = new List<Employee>(),
                JoinDate = DateTime.Now,
                MenuItems = new List<MenuItem>(),
                Orders = new List<Order>(),
                Payments = new List<Payment>()
            };

            var menuItems = new List<MenuItem>() {
                new MenuItem
                {
                    Name = "Snidane",
                    Company = company,
                    SellPrice = 10
                },
                new MenuItem
                {
                    Name = "Obed",
                    Company = company,
                    SellPrice = 20
                },
                new MenuItem
                {
                    Name = "Vecere",
                    Company = company,
                    SellPrice = 30
                }
            };

            var payments = new List<Payment>()
            {
                new Payment()
                {
                    Amount = 100,
                    Company = company,
                    DateOfPayment = DateTime.Now.AddDays(-10),
                    DueDate = DateTime.Now,
                    ReceivedAmount = 100,
                    VariableNumber = "111455"
                },
                new Payment()
                {
                    Amount = 100,
                    Company = company,
                    DateOfPayment = DateTime.Now.AddDays(-5),
                    DueDate = DateTime.Now,
                    ReceivedAmount = 100,
                    VariableNumber = "111455"
                },
                new Payment()
                {
                    Amount = 100,
                    Company = company,
                    DueDate = DateTime.Now.AddDays(5),
                    VariableNumber = "111455"
                }
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    Company = company,
                    OrderStartTime = DateTime.Now
                },
                new Order()
                {
                    Company = company,
                    OrderStartTime = DateTime.Now
                },
                new Order()
                {
                    Company = company,
                    OrderStartTime = DateTime.Now
                }
            };

            var orderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    IsPaid = false,
                    MenuItem = menuItems[0],
                    Order = orders[0]
                },
                new OrderItem()
                {
                    IsPaid = true,
                    MenuItem = menuItems[0],
                    Order = orders[0]
                },
                new OrderItem()
                {
                    IsPaid = false,
                    MenuItem = menuItems[1],
                    Order = orders[0]
                },
                new OrderItem()
                {
                    IsPaid = false,
                    MenuItem = menuItems[0],
                    Order = orders[1]
                },
                new OrderItem()
                {
                    IsPaid = false,
                    MenuItem = menuItems[2],
                    Order = orders[2]
                },
            };

            var employees = new List<Employee>()
            {
                new Employee()
                {
                    Company = company,
                    Email = "owner@company.com",
                    FirstName = "Owner",
                    LastName = "Serious",
                    PasswordHash = "asds",
                    Role = Role.Owner
                },
                new Employee()
                {
                    Company = company,
                    Email = "manager@company.com",
                    FirstName = "Manager",
                    LastName = "Serious",
                    PasswordHash = "asss",
                    Role = Role.Manager
                }
            };

            context.Companies.Add(company);
            context.MenuItems.AddRange(menuItems);
            context.Payments.AddRange(payments);
            context.Orders.AddRange(orders);
            context.Employees.AddRange(employees);
            context.OrderItems.AddRange(orderItems);

            context.SaveChanges();
            return context;
        }
    }
}
