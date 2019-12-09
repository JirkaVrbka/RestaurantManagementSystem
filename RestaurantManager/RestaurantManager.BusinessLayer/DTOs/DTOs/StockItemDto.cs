using System.Collections.Generic;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class StockItemDto : DtoBase
    {
        public int CompanyId { get; set; }
        public int BuyPrice { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
    }
}
