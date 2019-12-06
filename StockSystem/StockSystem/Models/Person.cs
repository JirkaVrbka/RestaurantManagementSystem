using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.DAL.Enums;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; }
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Persons);
    }
}
