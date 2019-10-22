using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public virtual List<ItemAmount> Items { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public virtual Company Company { get; set; }
    }
}
