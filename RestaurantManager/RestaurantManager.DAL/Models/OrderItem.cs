using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.DAL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.OrderItems);
    }
}
