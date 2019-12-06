using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RestaurantManager.DAL;
using RestaurantManager.DAL.Enums;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EntityFramework;
using RestaurantManager.Infrastructure.EntityFramework.UnitOfWork;

namespace Demo
{
    class Program
    {
        static async Task Main()
        {
            await CreateCompany2();
        }

        public static async Task CreateCompany2()
        {
            //using (var provider = new EntityFrameworkUnitOfWorkProvider(DbContextGenerator))
            //{
            //    //provider.Create();
            //    IRepository<Company> repository = new EntityFrameworkRepository<Company>(provider);
            //    using (var unitOfWork = provider.Create())
            //    {
            //        var company = new Company
            //        {
            //            Ico = 12345,
            //            Name = "Repository"
            //        };

            //        repository.Create(company);
            //        await unitOfWork.Commit();
            //    }
            //}

        }

        public static DbContext DbContextGenerator()
        {
            return new RestaurantManagerDbContext();
            ;
        }

        public static void CreateCompany()
        {
            var company = new Company();

            company.Ico = 12345;
            company.StockItems = CreateItems(company);
            company.People = CreatePersons(company);
            company.Name = "SomeCompany";

            using (var db = new RestaurantManagerDbContext())
            {
                db.Companies.Add(company);
                db.SaveChanges();
                db.Stocks.Add(CreateStock(db.Companies.First()));
                db.SaveChanges();
            }
        }

        public static Stock CreateStock(Company company)
        {
            var stock = new Stock()
            {
                //Company = company
            };

            using (var db = new RestaurantManagerDbContext())
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
            }

            return stock;
        }

        public static List<StockItem> CreateItems(Company company)
        {
            var items =  new List<StockItem>()
            {
                new StockItem()
                {
                    Company = company,
                    Name = "rohlik",
                    BuyPrice = 3,
                    SellPrice = 4,
                    Unit = Unit.Gram,
                    Amount = 100
                }
            };

            using (var db = new RestaurantManagerDbContext())
            {
                db.Items.AddRange(items);
                db.SaveChanges();
            }

            return items;
        }


        public static List<Person> CreatePersons(Company company)
        {
            var persons = new List<Person>()
            {
                new Person()
                {
                    FirstName = "Asdfg"
                }
            };
            using (var db = new RestaurantManagerDbContext())
            {
                db.Persons.AddRange(persons);
                db.SaveChanges();
            }

            return persons;
        }

        //public static PaymentInfo CreatePaymentInfoo(Company company)
        //{
        //    var info = new PaymentInfo()
        //    {
        //        Company = company,
        //        Amount = 12000,
        //        DueDate = new DateTime()
        //    };

        //    var repo = new EntityFrameworkRepository<PaymentInfo>(new EntityFrameworkUnitOfWorkProvider());
        //}
    }
}
