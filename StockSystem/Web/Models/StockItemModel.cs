using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantManager.DAL.Enums;

namespace Web.Models
{
    public class StockItemModel
    {
        public string Name { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public virtual Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}