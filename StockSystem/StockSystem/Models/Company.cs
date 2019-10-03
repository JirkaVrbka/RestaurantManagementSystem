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
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public List<Person> Persons { get; set; }
        public List<Role> Roles { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public List<Item> Items { get; set; }
        public List<Inventory> Inventories { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
