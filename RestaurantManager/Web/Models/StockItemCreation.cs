using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantManager.BusinessLayer.DTOs.DTOs;

namespace Web.Models
{
    public class StockItemCreation
    {
        public MenuItemDto MenuItem { get; set; }
        public StockItemDto StockItem { get; set; }
    }
}