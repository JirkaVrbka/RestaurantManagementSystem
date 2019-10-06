using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Company
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Location { get; set; }
        public int ICO { get; set; }
        public virtual List<Person> Persons { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual List<Inventory> Inventories { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
