using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RestaurantManager.BusinessLayer.Config;
using RestaurantManager.BusinessLayer.Facades.Common;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EF;
using RestaurantManager.Infrastructure.EF.UnitOfWork;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.Test.Config
{
    public class TestInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "RestaurantManagerBLTest";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new BusinessLayerInstaller());

            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new RestaurantManagerDbContext(TestDbConnectionString))
                    .LifestyleTransient()
                    .Named("Overriding DB context")
                    .IsDefault()
            );


        }

        private static DbContext InitializeDatabase()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            RestaurantManagerDbContext context = new RestaurantManagerDbContext(TestDbConnectionString);

            foreach (var c in context.Companies)
            {
                Console.WriteLine(c.Ico);
            }

            var company = new Company
            {
                Ico = 12355578,
                Name = "Panda",
                Employees = new List<Employee>(),
                JoinDate = DateTime.Now,
                MenuItems = new List<MenuItem>(),
                Orders = new List<Order>(),
                Payments = new List<Payment>(),
                Stock = new List<StockItem>()
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

            var stockItems = new List<StockItem>()
            {
                new StockItem()
                {
                    Amount = 5,
                    BuyPrice = 10,
                    Company = company
                },
                new StockItem()
                {
                    Amount = 52,
                    BuyPrice = 15,
                    Company = company
                },
                new StockItem()
                {
                    Amount = 4,
                    BuyPrice = 140,
                    Company = company
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
            context.StockItems.AddRange(stockItems);
            context.Orders.AddRange(orders);
            context.Employees.AddRange(employees);
            context.OrderItems.AddRange(orderItems);

            context.SaveChanges();
            return context;
        }
    }
}
