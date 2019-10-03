using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
