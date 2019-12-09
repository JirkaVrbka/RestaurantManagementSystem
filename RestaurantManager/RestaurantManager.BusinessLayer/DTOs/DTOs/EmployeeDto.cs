using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class EmployeeDto : DtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public int CompanyId { get; set; }
    }
}
