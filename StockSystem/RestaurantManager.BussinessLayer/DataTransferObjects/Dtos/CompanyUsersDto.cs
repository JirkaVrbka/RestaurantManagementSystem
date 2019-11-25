using System.Collections.Generic;

namespace RestaurantManager.BusinessLayer.DataTransferObjects.Dtos
{
    public class CompanyUsersDto : DtoBase
    {
        public List<PersonDto> People { get; set; }
    }
}
