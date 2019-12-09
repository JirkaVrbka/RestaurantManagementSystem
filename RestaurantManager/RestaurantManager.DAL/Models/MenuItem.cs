using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.DAL.Models
{
    public class MenuItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        public virtual Stock InStock { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.MenuItems);
    }
}
