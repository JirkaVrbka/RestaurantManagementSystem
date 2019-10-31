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
    public class ModelsUnitTest : UnitTestsBase
    {
        [TestMethod]
        public async Task CreateComplexModelTest()
        {
            // create company
            IRepository<Company> repositoryCompany = new EntityFrameworkRepository<Company>(Provider);
            Company company = getCompany();

            repositoryCompany.Create(company);
            await UnitOfWork.Commit();


            // create roles in company
            IRepository<Role> repositoryRoles = new EntityFrameworkRepository<Role>(Provider);
            var roles = getBasicRoles(company, Enum.GetValues(typeof(RolePermission)).Cast<RolePermission>().ToList());
            
            roles.ForEach(r => repositoryRoles.Create(r));
            await UnitOfWork.Commit();


            // create employees
            IRepository<Person> repositoryPeople = new EntityFrameworkRepository<Person>(Provider);
            List<Person> people = new List<Person>
            {
                getPerson(company, roles),
                getPerson(company, roles),
                getPerson(company, roles),
                getPerson(company, roles)
            };

            people.ForEach(p => repositoryPeople.Create(p));
            await UnitOfWork.Commit();
            

            // create items in company 
            IRepository<Item> repositoryItem = new EntityFrameworkRepository<Item>(Provider);
            List<Item> items = new List<Item>
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
            var stock = getStock(company, new List<ItemAmount>
            {
                getItemAmount(items.First()),
                getItemAmount(items.Last())
            });

            repositoryStock.Create(stock);
            await UnitOfWork.Commit();


            var dbCompany = repositoryCompany.GetAsync(company.Id, new string[]{ "Persons", "Items"}).Result;
            
            Assert.AreEqual(people.Count, dbCompany.Persons.Count);
            Assert.AreEqual(items.Count, dbCompany.Items.Count);
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

        private Item getItem(Company company, string name)
        {
            return new Item
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

        private ItemAmount getItemAmount(Item item)
        {
            return new ItemAmount
            {
                Item = item,
                ItemId = item.Id,
                Amount = 50
            };
        }

        private Order getOrder(Company company, List<ItemAmount> orderItemAmounts)
        {
            return new Order
            {
                Company = company, 
                CompanyId = company.Id,
                Items = orderItemAmounts
            };
        }

        private Payment getPayment(PaymentInfo paymentInfo)
        {
            return new Payment
            {
                PaymentInfo = paymentInfo,
                PaymentInfoId = paymentInfo.Id,
                Amount = 10,
                DateOfPayment = DateTime.Now
            };
        }

        private PaymentInfo getPaymentInfo(Company company, List<Payment> payments)
        {
            return new PaymentInfo
            {
                Company = company,
                CompanyId = company.Id,
                Amount = 200,
                DueDate = DateTime.Now.AddDays(10),
                Payments = payments
            };
        }

        private Person getPerson(Company company, List<Role> roles)
        {
            return new Person
            {
                Company = company,
                CompanyId = company.Id,
                FirstName = "Jenda" + new Random().Next(100),
                LastName = "Novak" + new Random().Next(100),
                Password = "abc" + new Random().Next(10000000),
                Roles = roles.ToList()
            };
        }

        private List<Role> getBasicRoles(Company company, List<RolePermission> rolePermissions)
        {
            return new List<Role>()
            {
                new Role
                {
                    Company = company,
                    CompanyId = company.Id,
                    Name = "Pracant",
                    Permissions = rolePermissions.GetRange(0,1)
                },
                new Role
                {
                    Company = company,
                    CompanyId = company.Id,
                    Name = "Veduci tej knapy",
                    Permissions = rolePermissions.GetRange(0,2)
                },
                new Role
                {
                    Company = company,
                    CompanyId = company.Id,
                    Name = "Ten co nas vsechny plati",
                    Permissions = rolePermissions
                }
            };
        }

        private Stock getStock(Company company, List<ItemAmount> items)
        {
            return new Stock
            {
                Company = company,
                CompanyId = company.Id,
                Items = items
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
