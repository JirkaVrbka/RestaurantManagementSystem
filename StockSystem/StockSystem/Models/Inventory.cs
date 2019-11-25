using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;
using StockSystem;

namespace RestaurantManager.DAL.Models
{
    public class Inventory : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Order> Orders { get; set; }
        public DateTime InventoryDate { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Inventories);
    }
}
