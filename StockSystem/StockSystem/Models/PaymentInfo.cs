using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;
using StockSystem;

namespace RestaurantManager.DAL.Models
{
    public class PaymentInfo : IEntity
    {
        [ForeignKey("Company")]
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual List<Payment> Payments { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.PaymentInfos);
    }
}
