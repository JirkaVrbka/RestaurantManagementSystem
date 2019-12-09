namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class OrderItemDto : DtoBase
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public bool IsPaid { get; set; }
    }
}
