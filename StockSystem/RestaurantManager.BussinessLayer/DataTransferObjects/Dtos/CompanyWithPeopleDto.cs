using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DataTransferObjects.Dtos
{
    public class CompanyWithPeopleDto : CompanyDto
    {
        public List<PersonDto> People { get; set; }
    }
}
