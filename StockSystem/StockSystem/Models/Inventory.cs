using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Inventory : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        public List<ItemAmount> Amount { get; set; }
        public virtual List<ItemAmount> BrokenItems { get; set; }
        public DateTime InventoryDate { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(Inventory);
    }
}
