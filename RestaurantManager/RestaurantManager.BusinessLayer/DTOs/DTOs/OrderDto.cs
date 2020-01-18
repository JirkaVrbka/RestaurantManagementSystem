using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class OrderDto : DtoBase
    {
        public int CompanyId { get; set; }
        public DateTime OrderStartTime { get; set; }
        public int OrderTable { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public bool IsClosed { get; set; }
    }
}
