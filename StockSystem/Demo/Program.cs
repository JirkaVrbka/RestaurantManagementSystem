using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;
using DAL.Models;
using StockSystem;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateCompany();

            
        }

        public static void CreateCompany()
        {
            var company = new Company();

            company.ICO = 12345;
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
    }
}
