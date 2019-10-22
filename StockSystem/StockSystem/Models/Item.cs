using System.ComponentModel.DataAnnotations;
using RestaurantManager.DAL.Enums;

namespace RestaurantManager.DAL.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public virtual Unit Unit { get; set; }
        public int Amount { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
    }
}
