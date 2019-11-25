using System.Collections.Generic;

namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    public class CompanyUsersDto : DtoBase
    {
        public List<PersonDto> People { get; set; }
    }
}
