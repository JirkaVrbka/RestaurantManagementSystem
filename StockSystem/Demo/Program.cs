using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;
using DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EntityFramework;
using RestaurantManager.Infrastructure.EntityFramework.UnitOfWork;
using StockSystem;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            CreateCompany2();

            
        }

        public static async Task CreateCompany2()
        {
            using (var provider = new EntityFrameworkUnitOfWorkProvider(DbContextGenerator))
            {
                //provider.Create();
                IRepository<Company> repository = new EntityFrameworkRepository<Company>(provider);
                using (var unitOfWork = provider.Create())
                {
                    var company = new Company
                    {
                        Ico = 12345,
                        Location = "Praha",
                        Name = "Repository"
                    };

                    repository.Create(company);
                    await unitOfWork.Commit();
                }
            }

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
            company.Items = CreateItems(company);
            company.Location = "Brno";
            company.Roles = CreateRoles(company);
            company.Persons = CreatePersons(company);
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

        public static List<Item> CreateItems(Company company)
        {
            var items =  new List<Item>()
            {
                new Item()
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

        public static List<Role> CreateRoles(Company company)
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Manager",
                    Company = company
                }
            };
            using (var db = new RestaurantManagerDbContext())
            {
                db.Roles.AddRange(roles);
                db.SaveChanges();
            }

            return roles;
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

        public static PaymentInfo CreatePaymentInfo(Company company)
        {
            var info = new PaymentInfo()
            {
                Company = company,
                Amount = 12000,
                DueDate = new DateTime()
            };

            using (var db = new RestaurantManagerDbContext())
            {
                db.PaymentInfos.Add(info);
                db.SaveChanges();
            }

            return info;
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
