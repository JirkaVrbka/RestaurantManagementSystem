using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.DAL.Models
{
    public class Employee : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "An employee has to have a role in company")]
        public Role Role { get; set; }


        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Employees);
    }
}
