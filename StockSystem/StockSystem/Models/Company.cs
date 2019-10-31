using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.DAL.Models
{
    public class Company : IEntity
    {
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Location { get; set; }
        public int Ico { get; set; }
        public virtual List<Person> Persons { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual List<Inventory> Inventories { get; set; }
        public DateTime? JoinDate { get; set; }
        [NotMapped]
        public string TableName { get; } = nameof(Company);
    }
}
