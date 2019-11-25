using RestaurantManager.DAL.Enums;

namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    class ItemSellingInfoDto : DtoBase
    {
        public string Name { get; set; }
        public int SellPrice { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}
