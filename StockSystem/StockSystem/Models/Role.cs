using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RestaurantManager.DAL.Enums;

namespace RestaurantManager.DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<RolePermission> Permissions { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
    }
}
