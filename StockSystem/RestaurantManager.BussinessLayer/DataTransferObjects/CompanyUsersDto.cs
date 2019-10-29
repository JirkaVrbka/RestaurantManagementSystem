using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects
{
    public class CompanyUsersDto : DtoBase
    {
        //TODO this does error
        public List<UserDto> People { get; set; }
    }
}
