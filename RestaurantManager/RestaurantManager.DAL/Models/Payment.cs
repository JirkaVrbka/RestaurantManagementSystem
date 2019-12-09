using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Payment : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [Required]
        public int Amount { get; set; }
        public int ReceivedAmount { get; set; }
        [Required]
        public string VariableNumber { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public DateTime? DueDate { get; set; }

        [NotMapped] 
        public bool IsPaid => this.ReceivedAmount >= this.Amount;

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Payments);
    }
}
