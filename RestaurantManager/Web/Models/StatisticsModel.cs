using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class StatisticsModel
    {
        public Dictionary<string, int> ItemsSellOverall { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ItemsSellLastMonth { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ItemsSellLastToday { get; set; } = new Dictionary<string, int>();
        public int CostsAll { get; set; } = 0;
        public int CostsMonth { get; set; } = 0;
        public int CostsToday { get; set; } = 0;

        public int RevenuesAll { get; set; } = 0;
        public int RevenuesMonth { get; set; } = 0;
        public int RevenuesToday { get; set; } = 0;
    }
}