using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ItemAmount
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
    }
}
