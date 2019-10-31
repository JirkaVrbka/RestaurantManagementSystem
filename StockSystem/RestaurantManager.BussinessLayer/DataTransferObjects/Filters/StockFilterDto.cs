using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects.Filters
{
    public class StockFilterDto : FilterDtoBase
    {
        public int StockId { get; set; }
    }
}
