using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using RestaurantManager.Utils.EntityEnums;

namespace Web.Models
{
    public class EmployeeModel
    {
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}