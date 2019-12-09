using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.EF.Test.Config;
using RestaurantManager.Infrastructure.EF.UnitOfWork;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.Infrastructure.EF.Test
{
    [TestClass]
    public class RepositoryTest
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RestaurantManagerDbContext>());
            
            Container.Install(new EFTestInstaller());
        }


        [TestMethod]
        public async Task SimpleSingleCompany()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            IRepository<Company> companyRepository = Container.Resolve<IRepository<Company>>();


            using (unitOfWorkProvider.Create())
            {
                companyRepository.Create(new Company()
                {
                    Ico = 11111111,
                    JoinDate = DateTime.Now,
                    Name = "Koala"
                });

                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();
                Company firstCompany = await companyRepository.GetAsync(1);
                Company secondCompany = await companyRepository.GetAsync(2);

                Assert.AreEqual("Panda", firstCompany.Name);
                Assert.AreEqual("Koala", secondCompany.Name);

            }
        }

        [TestMethod]
        public async Task CreateSingleCompanyWithDependencies()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();

            var company = new Company
            {
                Ico = 12345678,
                Name = "Bear",
                Employees = new List<Employee>(),
                JoinDate = DateTime.Now,
                MenuItems = new List<MenuItem>(),
                Orders = new List<Order>(),
                Payments = new List<Payment>(),
                Stock = new List<Stock>()
            };

            var menuItems = new List<MenuItem>() {
                new MenuItem
                {
                    Name = "Snidane",
                    Company = company,
                    Price = 10
                },
                new MenuItem
                {
                    Name = "Obed",
                    Company = company,
                    Price = 20
                },
                new MenuItem
                {
                    Name = "Vecere",
                    Company = company,
                    Price = 30
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

            var stockItems = new List<Stock>()
            {
                new Stock()
                {
                    MenuItem = menuItems[0],
                    Amount = 5,
                    BuyPrice = 10,
                    Company = company
                },
                new Stock()
                {
                    MenuItem = menuItems[1],
                    Amount = 52,
                    BuyPrice = 15,
                    Company = company
                },
                new Stock()
                {
                    MenuItem = menuItems[2],
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
                    HashedPassword = "asds",
                    Role = Role.Owner
                },
                new Employee()
                {
                    Company = company,
                    Email = "manager@company.com",
                    FirstName = "Manager",
                    LastName = "Serious",
                    HashedPassword = "asss",
                    Role = Role.Manager
                }
            };


            IRepository<Company> companyRepository = Container.Resolve<IRepository<Company>>();
            IRepository<MenuItem> menuItemRepository = Container.Resolve<IRepository<MenuItem>>();
            IRepository<Payment> paymentRepository = Container.Resolve<IRepository<Payment>>();
            IRepository<Stock> stockRepository = Container.Resolve<IRepository<Stock>>();
            IRepository<Order> orderRepository = Container.Resolve<IRepository<Order>>();
            IRepository<OrderItem> orderItemRepository = Container.Resolve<IRepository<OrderItem>>();
            IRepository<Employee> employeeRepository = Container.Resolve<IRepository<Employee>>();

            using (unitOfWorkProvider.Create())
            {
                companyRepository.Create(company);
                menuItems.ForEach(mi => menuItemRepository.Create(mi));
                payments.ForEach(mi => paymentRepository.Create(mi));
                stockItems.ForEach(mi => stockRepository.Create(mi));
                orders.ForEach(mi => orderRepository.Create(mi));
                orderItems.ForEach(mi => orderItemRepository.Create(mi));
                employees.ForEach(mi => employeeRepository.Create(mi));

                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();

                company = await companyRepository.GetAsync(2, new string[] { "MenuItems", "Orders", "Employees" });

            }

            Assert.AreEqual(3, company.MenuItems.Count);
            Assert.AreEqual(3, company.Orders.Count);
            Assert.AreEqual("Bear", company.Name);
            Assert.AreEqual(2, company.Employees.Count);
        }

        [TestMethod]
        public async Task SimpleUpdate()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            IRepository<Company> companyRepository = Container.Resolve<IRepository<Company>>();

            using (unitOfWorkProvider.Create())
            {

                Company company = await companyRepository.GetAsync(1);
                company.Name = "Horse";
                
                companyRepository.Update(company);
                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();

                company = await companyRepository.GetAsync(1);

                Assert.AreEqual("Horse", company.Name);
            }

        }


        [TestMethod]
        public async Task SimpleDelete()
        {
            IUnitOfWorkProvider unitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            IRepository<Company> companyRepository = Container.Resolve<IRepository<Company>>();

            using (unitOfWorkProvider.Create())
            {
                companyRepository.Delete(1);
                await unitOfWorkProvider.GetUnitOfWorkInstance().Commit();

                Company company = await companyRepository.GetAsync(1);

                Assert.IsNull(company);
            }
        }


    }
}
