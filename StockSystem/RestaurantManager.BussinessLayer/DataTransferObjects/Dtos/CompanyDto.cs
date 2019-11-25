using System;

namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    public class CompanyDto : DtoBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Ico { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
