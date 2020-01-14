using System;
using System.Collections.Generic;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class OrderWithFullDependencyDto : DtoBase
    {
        public int CompanyId { get; set; }
        public DateTime OrderStartTime { get; set; }
        public int OrderTable { get; set; }
        public List<OrderItemWithMenuItemDto> Items { get; set; }
    }
}
