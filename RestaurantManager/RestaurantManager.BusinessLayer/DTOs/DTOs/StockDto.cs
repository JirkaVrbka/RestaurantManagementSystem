using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BuyPrice { get; set; }
        public int Amount { get; set; }
    }
}
