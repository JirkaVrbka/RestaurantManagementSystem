using System;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class CompanyDto : DtoBase
    {
        public string Name { get; set; }
        public int Ico { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
