using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;
using StockSystem;

namespace RestaurantManager.DAL.Models
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int Amount { get; set; }
        public int ReceivedAmount { get; set; }
        public string VariableNumber { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public DateTime? DueDate { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Payments);
    }
}
