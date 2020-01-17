using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public DateTime OrderStartTime { get; set; }
        public int OrderTable { get; set; }
        public bool isPaid { get; set; }

    }
}