using System;

namespace RestaurantManager.BusinessLayer.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime OrderStartTime { get; set; }

    }
}
