using System.ComponentModel.DataAnnotations;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class EmployeeDto : DtoBase
    {
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
        public Role Role { get; set; }

        public int CompanyId { get; set; }
    }
}
