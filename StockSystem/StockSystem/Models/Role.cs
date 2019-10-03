using System;
using System.Collections.Generic;
using System.Text;
using DAL.Enums;

namespace DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RolePermission> Permissions { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
