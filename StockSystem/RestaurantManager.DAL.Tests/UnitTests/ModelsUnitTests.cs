using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL.Enums;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EntityFramework;

namespace RestaurantManager.DAL.Tests.UnitTests
{
    [TestClass]
    public class ModelsUnitTests : UnitTestsBase
    {
        [TestMethod]
        public async Task CreateComplexModelTest()
        {
            // create company
            IRepository<Company> repositoryCompany = new EntityFrameworkRepository<Company>(Provider);
            Company company = getCompany();

            repositoryCompany.Create(company);
            await UnitOfWork.Commit();
            

            // create employees
            IRepository<Person> repositoryPeople = new EntityFrameworkRepository<Person>(Provider);
            List<Person> people = new List<Person>
            {
                getPerson(company, Role.Employee),
                getPerson(company, Role.Employee),
                getPerson(company, Role.Manager),
                getPerson(company, Role.Owner)
            };

            people.ForEach(p => repositoryPeople.Create(p));
            await UnitOfWork.Commit();
            

            // create items in company 
            IRepository<StockItem> repositoryItem = new EntityFrameworkRepository<StockItem>(Provider);
            List<StockItem> items = new List<StockItem>
            {
                getItem(company, "vodka"),
                getItem(company, "nespavost"),
                getItem(company, "deprese"),
                getItem(company, "zoufalstvi"),
            };

            items.ForEach(i => repositoryItem.Create(i));
            await UnitOfWork.Commit();

            // create stock to company
            // check if itemAmount will be created 
            IRepository<Stock> repositoryStock = new EntityFrameworkRepository<Stock>(Provider);
            var stock = getStock(company, new List<MenuItemAmount>
            {
                //getItemAmount(items.First()),
                //getItemAmount(items.Last())
            });

            repositoryStock.Create(stock);
            await UnitOfWork.Commit();


            var dbCompany = repositoryCompany.GetAsync(company.Id, new string[]{ "People", "Items"}).Result;
            
            Assert.AreEqual(people.Count, dbCompany.People.Count);
            Assert.AreEqual(items.Count, dbCompany.MenuItems.Count);
        }

        private Company getCompany()
        {
            return DefaultCompany;
        }

        private Inventory getInventory(Company company)
        {
            return new Inventory
            {
                Company = company,
                CompanyId = company.Id
            };
        }

        private StockItem getItem(Company company, string name)
        {
            return new StockItem
            {
                Company = company,
                CompanyId = company.Id,
                Name = name,
                BuyPrice = 10,
                SellPrice = 20,
                Amount = 200,
                Unit = Unit.Milliliter
            };
        }
        /*
        private MenuItemAmount getItemAmount(StockItem item)
        {
            return new MenuItemAmount
            {
                Item = item,
                ItemId = item.Id,
                Amount = 50
            };
        }*/

        private Order getOrder(Company company, List<MenuItemAmount> orderItemAmounts)
        {
            return new Order
            {
                Company = company, 
                CompanyId = company.Id,
                Items = orderItemAmounts
            };
        }

        private Payment getPayment(Company company)
        {
            return new Payment
            {
                Company = company,
                Amount = 10,
                DateOfPayment = DateTime.Now
            };
        }


        private Person getPerson(Company company, Role role)
        {
            return new Person
            {
                Company = company,
                CompanyId = company.Id,
                FirstName = "Jenda" + new Random().Next(100),
                LastName = "Novak" + new Random().Next(100),
                HashedPassword = "abc" + new Random().Next(10000000),
                Role = role
            };
        }

        private Stock getStock(Company company, List<MenuItemAmount> items)
        {
            return new Stock
            {
                CompanyId = company.Id,
                //Items = items
            };
        }
        [TestMethod]
        public async Task UpdateCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(Provider);

            DefaultCompany.Name = "Karlova tovarna na cokoladu";
            repository.Update(DefaultCompany);
            await UnitOfWork.Commit();
            var dbCompany = repository.GetAsync(DefaultCompany.Id).Result;

            Assert.AreEqual(DefaultCompany.Name, dbCompany.Name);
            Assert.AreEqual(DefaultCompany.Ico, dbCompany.Ico);
            Assert.AreEqual(DefaultCompany.JoinDate.ToString(), dbCompany.JoinDate.ToString());
        }

        [TestMethod]
        public async Task DeleteCompanyTest()
        {
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(Provider);

            repository.Delete(DefaultCompany.Id);
            await UnitOfWork.Commit();
            var dbCompany = await repository.GetAsync(DefaultCompany.Id);

            Assert.AreEqual(null, dbCompany);
        }

        
    }
}
