using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.Filters
{
    public class MenuItemFilterDto : FilterDtoBase
    {
        public int CompanyId { get; set; }
    }
}
