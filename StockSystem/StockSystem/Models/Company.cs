using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;
using StockSystem;

namespace RestaurantManager.DAL.Models
{
    public class Company : IEntity
    {
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Ico { get; set; }
        public virtual List<Person> Persons { get; set; }
        public virtual List<MenuItem> MenuItems { get; set; }
        public virtual List<Inventory> Inventories { get; set; }
        public int InventoryId { get; set; }
        public virtual Stock Stock { get; set; }
        public int StockId { get; set; }
        public int PaymentInfoId { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public DateTime? JoinDate { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Companies);
        public int AmountOfTables { get; set; }
    }
}
