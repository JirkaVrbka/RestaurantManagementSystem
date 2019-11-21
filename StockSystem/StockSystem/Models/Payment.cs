using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;
using StockSystem;

namespace RestaurantManager.DAL.Models
{
    //TODO rozdelil jsem payment na payment info a pak jednotlive platby, kdzytak to checkni jestli to tak sedi
    public class Payment : IEntity
    { 
        public int Id { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public int Amount { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Payments);
    }
}
