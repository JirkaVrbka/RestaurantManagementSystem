using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public List<ItemAmount> Items { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
