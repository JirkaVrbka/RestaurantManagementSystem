using System;
using System.Data.Entity;
using DAL.Models;

namespace StockSystem
{
    public class StockSystemDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemAmount> ItemAmounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
    }
}
