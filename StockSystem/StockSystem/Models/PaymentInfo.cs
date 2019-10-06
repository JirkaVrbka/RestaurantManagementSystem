using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class PaymentInfo
    {
        [ForeignKey("Company")]
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual List<Payment> Payments { get; set; }
        [Required]
        public virtual Company Company { get; set; }
    }
}
