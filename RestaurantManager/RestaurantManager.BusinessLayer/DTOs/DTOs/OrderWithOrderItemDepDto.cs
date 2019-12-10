using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class OrderWithOrderItemDepDto : DtoBase
    {
        public int CompanyId { get; set; }
        public DateTime OrderStartTime { get; set; }
        public int OrderTable { get; set; }
        public List<OrderItemWithMenuItemDto> Items { get; set; }
    }
}
