using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class CompanyWithStockItemsDto : CompanyDto
    {
        public virtual List<StockItemDto> Stock { get; set; }
    }
}
