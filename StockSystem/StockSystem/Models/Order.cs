using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StockSystem;

namespace RestaurantManager.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual List<ItemAmount> Items { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Orders);
    }
}
