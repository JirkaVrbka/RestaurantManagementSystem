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
        public int SellPrice { get; set; }

        [Required]
        public int BuyPrice { get; set; }
        [Required]
        public int Amount { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.MenuItems);
    }
}
