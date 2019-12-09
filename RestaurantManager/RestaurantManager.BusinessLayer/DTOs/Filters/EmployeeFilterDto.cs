using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.Filters
{
    public class EmployeeFilterDto : FilterDtoBase
    {
        public string Email { get; set; }
        public int CompanyId { get; set; }
    }
}
