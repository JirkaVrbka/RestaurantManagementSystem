using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class MenuItem : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        public virtual StockItem InStockItem { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.MenuItems);
    }
}
