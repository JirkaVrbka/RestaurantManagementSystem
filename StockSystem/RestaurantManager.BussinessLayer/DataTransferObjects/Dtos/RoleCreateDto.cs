using System.Collections.Generic;

namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    public class RoleCreateDto
    {
        public string Name { get; set; }
        public virtual List<RolePermission> Permissions { get; set; }
        public int CompanyId { get; set; }
    }
}
