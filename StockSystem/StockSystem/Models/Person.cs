using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Role> Roles { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(Person);
    }
}
