using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class MenuItemModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public List<StockItemModel> Items { get; set; }
    }
}