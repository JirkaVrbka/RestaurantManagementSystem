using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class CompanyWithEmployeesDto : CompanyDto
    {
        public virtual List<EmployeeDto> Employees { get; set; }
    }
}
