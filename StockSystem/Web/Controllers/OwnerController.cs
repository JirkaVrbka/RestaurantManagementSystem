using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.DAL.Enums;
using Web.Models;

namespace Web.Controllers
{
    public class OwnerController : Controller
    {
        // GET: Owner

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuItems()
        {
            var menuItems = new List<MenuItemModel>()
            {
                new MenuItemModel()
                {
                    Name = "Fried cheese with fries",
                    Price = 100,
                    Items = new List<StockItemModel>()
                    {
                        new StockItemModel()
                        {
                            Name = "Potato",
                            Amount = 1,
                            Unit = Unit.Kilogram
                        },
                        new StockItemModel()
                        {
                            Name = "Cheese",
                            Amount = 2,
                            Unit = Unit.Kilogram,
                        }
                    }
                }
            };

            return View(menuItems);
        }

        public ActionResult Stock()
        {
            var itemsTest = new List<StockItemModel>()
            {
                new StockItemModel()
                {
                    Name = "Beer",
                    Amount = 5,
                    BuyPrice = 20,
                    SellPrice = 30,
                    Unit = Unit.Liter
                },
                new StockItemModel()
                {
                    Name = "Potato",
                    Amount = 10,
                    BuyPrice = 5,
                    SellPrice = 12,
                    Unit = Unit.Kilogram
                },
                new StockItemModel()
                {
                    Name = "Meat",
                    Amount = 2,
                    Unit = Unit.Kilogram,
                    BuyPrice = 12,
                    SellPrice = 16
                }
            };

            return View(itemsTest);
        }
        public ActionResult Statistics()
        {
            return View();
        }

        public ActionResult Employees()
        {
            var employeesTest = new List<EmployeeModel>()
            {
                new EmployeeModel()
                {
                    Role = Role.Manager,
                    Email = "someEmail@email.com",
                    FirstName = "Carl",
                    LastName = "Newman"
                },
                new EmployeeModel()
                {
                    Role = Role.Employee,
                    Email = "someEmail2@email.com",
                    FirstName = "John",
                    LastName = "Wick"
                }
            };
            return View(Employees());
        }

        public ActionResult Payments()
        {
            var paymentsTest = new List<PaymentModel>()
            {
                new PaymentModel()
                {
                    Amount = 800,
                    DueDate = new DateTime(2019, 12, 10),
                    PayDay = new DateTime(2019, 12, 1),
                    Paid = false
                },
                new PaymentModel()
                {
                    Amount = 1020,
                    DueDate = new DateTime(2019, 11, 10),
                    PayDay = new DateTime(2019, 11, 1),
                    Paid = true
                }
            };

            return View(paymentsTest);
        }
        public ActionResult MyCompany()
        {
            var companyTest = new CompanyModel()
            {
                Name = "Facebook",
                Ico = 12345,
                Location = "Brno"
            };
            return View(companyTest);
        }
        public ActionResult MyAccount()
        {
            var accountTest = new MyAccountModel()
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "jsmith@email.com"
            };
            return View(accountTest);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}