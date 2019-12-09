using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.DAL.Models
{
    public class Stock
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
