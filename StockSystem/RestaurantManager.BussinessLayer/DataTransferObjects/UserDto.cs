using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects
{
    public class UserDto : DtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
