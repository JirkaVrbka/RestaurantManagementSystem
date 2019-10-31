using RestaurantManager.DAL.Enums;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class ItemAmount : IEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int Amount { get; set; }
    }
}
