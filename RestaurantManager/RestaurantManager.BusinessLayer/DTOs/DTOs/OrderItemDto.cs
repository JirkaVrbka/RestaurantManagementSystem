namespace RestaurantManager.BusinessLayer.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public bool IsPaid { get; set; }
    }
}
