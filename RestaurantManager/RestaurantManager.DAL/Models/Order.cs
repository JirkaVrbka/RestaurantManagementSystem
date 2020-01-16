using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Order : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<OrderItem> Items { get; set; }
        [Required]
        public DateTime OrderStartTime { get; set; }
        [Required]
        public int OrderTable { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Orders);
    }
}
