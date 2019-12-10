using System;
using System.Collections.Generic;
using System.Data.Entity;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL.Models;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.DAL.Test
{
    [TestClass]
    public class SimpleDatabaseManipulationViaContextTest
    {
        private const string TestDbConnectionString = "RestaurantManagerDALTest";

        [TestMethod]
        public void CreateSingleCompany()
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
                Stock = new List<StockItem>()
            };

            context.Companies.Add(company);
            context.SaveChanges();
        }

        [TestMethod]
        public void CreateSingleCompanyWithDependencies()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            RestaurantManagerDbContext context = new RestaurantManagerDbContext(TestDbConnectionString);

            var company = new Company
            {
                Id = 1,
                Ico = 12345678,
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
                    Id = 0,
                    Name = "Snidane",
                    Company = company,
                    SellPrice = 10
                },
                new MenuItem
                {
                    Id = 1,
                    Name = "Obed",
                    Company = company,
                    SellPrice = 20
                },
                new MenuItem
                {
                    Id = 2,
                    Name = "Vecere",
                    Company = company,
                    SellPrice = 30
                }
            };

            var payments = new List<Payment>()
            {
                new Payment()
                {
                    Id = 0,
                    Amount = 100,
                    Company = company,
                    DateOfPayment = DateTime.Now.AddDays(-10),
                    DueDate = DateTime.Now,
                    ReceivedAmount = 100,
                    VariableNumber = "111455"
                },
                new Payment()
                {
                    Id = 1,
                    Amount = 100,
                    Company = company,
                    DateOfPayment = DateTime.Now.AddDays(-5),
                    DueDate = DateTime.Now,
                    ReceivedAmount = 100,
                    VariableNumber = "111455"
                },
                new Payment()
                {
                    Id = 2,
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
                    Id = 0,
                    Amount = 5,
                    BuyPrice = 10,
                    Company = company
                },
                new StockItem()
                {
                    Id = 1,
                    Amount = 52,
                    BuyPrice = 15,
                    Company = company
                },
                new StockItem()
                {
                    Id = 2,
                    Amount = 4,
                    BuyPrice = 140,
                    Company = company
                }
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 0,
                    Company = company,
                    OrderStartTime = DateTime.Now
                },
                new Order()
                {
                    Id = 1,
                    Company = company,
                    OrderStartTime = DateTime.Now
                },
                new Order()
                {
                    Id = 2,
                    Company = company,
                    OrderStartTime = DateTime.Now
                }
            };

            var orderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Id = 0,
                    IsPaid = false,
                    MenuItem = menuItems[0],
                    Order = orders[0]
                },
                new OrderItem()
                {
                    Id = 1,
                    IsPaid = true,
                    MenuItem = menuItems[0],
                    Order = orders[0]
                },
                new OrderItem()
                {
                    Id = 2,
                    IsPaid = false,
                    MenuItem = menuItems[1],
                    Order = orders[0]
                },
                new OrderItem()
                {
                    Id = 3,
                    IsPaid = false,
                    MenuItem = menuItems[0],
                    Order = orders[1]
                },
                new OrderItem()
                {
                    Id = 4,
                    IsPaid = false,
                    MenuItem = menuItems[2],
                    Order = orders[2]
                },
            };

            var employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 0,
                    Company = company,
                    Email = "owner@company.com",
                    FirstName = "Owner",
                    LastName = "Serious",
                    PasswordHash = "asds",
                    Role = Role.Owner
                },
                new Employee()
                {
                    Id = 1,
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

            Assert.AreEqual(3, context.Companies.Find(1).MenuItems.Count);
            Assert.AreEqual(3, context.Companies.Find(1).Orders.Count);
            Assert.AreEqual(3, context.Companies.Find(1).Orders[0].Items.Count);
            Assert.AreEqual("Panda", context.Companies.Find(1).Name);
            Assert.AreEqual(2, context.Companies.Find(1).Employees.Count);
        }
    }
}
