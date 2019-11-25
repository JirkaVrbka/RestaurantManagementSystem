using System;
using System.Collections.Generic;

namespace RestaurantManager.BusinessLayer.DataTransferObjects.Dtos
{
    public class CompanyDto : DtoBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Ico { get; set; }
        public DateTime? JoinDate { get; set; }
        public List<PersonDto> People { get; set; }
    }
}
