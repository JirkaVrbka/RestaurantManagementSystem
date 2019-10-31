using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects
{
    public class CompanyDto : DtoBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Ico { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
