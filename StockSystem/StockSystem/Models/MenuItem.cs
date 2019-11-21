using System;
using System.Collections.Generic;
using System.Text;
using RestaurantManager.Infrastructure;
using StockSystem;

namespace RestaurantManager.DAL.Models
{
    public class MenuItem : IEntity
    {
        public int Id { get; set; }
        public string TableName { get; } = nameof(RestaurantManagerDbContext.MenuItems);
        public string Name { get; set; }
        public int Price { get; set; }
        public List<MenuItemAmount> Items { get; set; }
    }
}
