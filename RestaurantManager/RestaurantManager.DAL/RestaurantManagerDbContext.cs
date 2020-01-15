using System;
using System.Data.Entity;
using RestaurantManager.DAL.Config;
using RestaurantManager.DAL.Models;

namespace RestaurantManager.DAL
{
    public class RestaurantManagerDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public RestaurantManagerDbContext() : base(EntityFrameworkInstaller.ConnectionString)
        {
            // force load of EntityFramework.SqlServer.dll into build
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            // TODO remove when release
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public RestaurantManagerDbContext(string connectionString) : base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        /**
         * Add relationship for OrderItem without linking from MenuItem
         */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderItem>().HasRequired(o => o.MenuItem).WithMany().WillCascadeOnDelete(false);
        }
    }
}
