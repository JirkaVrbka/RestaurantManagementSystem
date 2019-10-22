using RestaurantManager.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects
{
    class RoleCreateDto
    {
        public string Name { get; set; }
        public virtual List<RolePermission> Permissions { get; set; }
        public int CompanyId { get; set; }
    }
}
