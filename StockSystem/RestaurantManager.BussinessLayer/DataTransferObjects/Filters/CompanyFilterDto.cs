using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects.Filters
{
    public class CompanyFilterDto : FilterDtoBase
    {
        public string Name { get; set; }
    }
}
