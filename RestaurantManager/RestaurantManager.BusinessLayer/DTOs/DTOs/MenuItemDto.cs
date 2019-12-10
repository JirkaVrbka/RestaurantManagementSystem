namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class MenuItemDto : DtoBase
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }
        public int Amount { get; set; }
    }
}
