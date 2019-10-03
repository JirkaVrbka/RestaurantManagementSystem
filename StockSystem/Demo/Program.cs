using System;
using System.Collections.Generic;
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

            company.Stock = CreateStock(company);
            company.ICO = 12345;
            company.Items = CreateItems(company);
            company.Location = "Brno";
            company.Roles = CreateRoles(company);
            company.Persons = CreatePersons(company);
            company.Name = "SomeCompany";
            company.PaymentInfo = CreatePaymentInfo(company);

            using (var db = new StockSystemDbContext())
            {
                db.Companies.Add(company);
                db.SaveChanges();
            }
        }

        public static Stock CreateStock(Company company)
        {
            var stock = new Stock()
            {
                Company = company
            };

            using (var db = new StockSystemDbContext())
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

            using (var db = new StockSystemDbContext())
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
            using (var db = new StockSystemDbContext())
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
            using (var db = new StockSystemDbContext())
            {
                db.Persons.Add(persons);
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

            using (var db = new StockSystemDbContext())
            {
                db.PaymentInfos.Add(info);
                db.SaveChanges();
            }

            return info;
        }
    }
}
