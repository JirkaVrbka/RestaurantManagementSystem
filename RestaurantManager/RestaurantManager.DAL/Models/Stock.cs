using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Stock : IEntity
    {
        [ForeignKey("MenuItem")]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        [Required]
        public int BuyPrice { get; set; }
        [Required]
        public int Amount { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.StockItems);


    }
}
