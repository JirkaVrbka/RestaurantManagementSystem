using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class EmployeeCreateDto : DtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Role Role { get; set; }

        public CompanyDto Company { get; set; }
    }
}
