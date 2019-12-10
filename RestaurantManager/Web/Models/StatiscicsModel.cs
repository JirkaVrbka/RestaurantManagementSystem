using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class StatiscicsModel
    {
        public int OrdersCountAll { get; set; }
        public int OrdersCountToday { get; set; }
        public int BuyPrices { get; set; }
        public int SellPrices { get; set; }
    }
}