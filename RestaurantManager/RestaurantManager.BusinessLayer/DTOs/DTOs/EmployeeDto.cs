using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; }

        public int CompanyId { get; set; }
    }
}
