using RestaurantManager.DAL.Enums;

namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    public class PersonDto : DtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; }
    }
}
