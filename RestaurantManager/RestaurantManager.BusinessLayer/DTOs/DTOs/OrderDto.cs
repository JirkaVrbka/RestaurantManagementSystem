using System;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class OrderDto : DtoBase
    {
        public int CompanyId { get; set; }
        public DateTime OrderStartTime { get; set; }

    }
}
