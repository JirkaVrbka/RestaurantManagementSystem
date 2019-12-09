namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class MenuItemDto : DtoBase
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
