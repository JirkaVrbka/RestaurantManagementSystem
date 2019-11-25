using System;
using System.Collections.Generic;

namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    public class PaymentInfoDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public List<PaymentDto> Payments { get; set; }
        public int CompanyId { get; set; }
    }
}
