using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Company : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is mandatory for a company")]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Range(10000000, 99999999, ErrorMessage = "Ico has to be 8 digits")]
        public int Ico { get; set; }
        public virtual List<Employee> Employees { get; set; }
        public virtual List<MenuItem> MenuItems { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Stock> Stock { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public DateTime? JoinDate { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(RestaurantManagerDbContext.Companies);
    }
}
