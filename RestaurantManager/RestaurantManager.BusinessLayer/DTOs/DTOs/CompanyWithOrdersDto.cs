using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class CompanyWithOrdersDto : CompanyDto
    {
        public virtual List<OrderDto> Orders { get; set; }
    }
}
