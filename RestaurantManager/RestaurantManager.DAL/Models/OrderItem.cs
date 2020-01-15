using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class OrderItem : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required]
        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.OrderItems);
    }
}
