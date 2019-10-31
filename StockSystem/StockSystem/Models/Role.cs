using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.DAL.Enums;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<RolePermission> Permissions { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(Role);
    }
}
