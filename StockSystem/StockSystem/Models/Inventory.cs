using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        public virtual List<ItemAmount> BrokenItems { get; set; }
    }
}
