using System;

namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    public class PaymentDto : DtoBase
    {
        public DateTime? DateOfPayment { get; set; }
        public int Amount { get; set; }
    }
}
