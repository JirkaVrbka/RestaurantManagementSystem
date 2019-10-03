using System;
using System.Collections.Generic;
using System.Text;
using DAL.Enums;

namespace DAL.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
