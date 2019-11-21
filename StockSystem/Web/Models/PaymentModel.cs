using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PaymentModel
    {
        public DateTime PayDay { get; set; }
        public DateTime DueDate { get; set; }
        public int Amount { get; set; }
        public bool Paid { get; set; }
        
    }
}