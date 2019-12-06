using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.DAL.Enums;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class MenuItemAmount : IEntity
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public virtual MenuItem Item { get; set; }
        public int Amount { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.ItemAmounts);

    }
}
