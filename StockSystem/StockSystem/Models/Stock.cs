using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Stock : IEntity
    {
        public int Id { get; set; }
        public virtual List<ItemAmount> Items { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(Stock);
    }
}
