using System;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int Amount { get; set; }
        public int ReceivedAmount { get; set; }
        public string VariableNumber { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsPaid => this.ReceivedAmount >= this.Amount;
    }
}
