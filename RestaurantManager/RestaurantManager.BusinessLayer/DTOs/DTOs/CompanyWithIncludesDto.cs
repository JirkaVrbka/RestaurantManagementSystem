using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class CompanyWithIncludesDto : CompanyDto
    {
        public virtual List<EmployeeDto> Employees { get; set; }
        public virtual List<MenuItemDto> MenuItems { get; set; }
        public virtual List<OrderDto> Orders { get; set; }
        public virtual List<StockDto> Stock { get; set; }
        public virtual List<PaymentDto> Payments { get; set; }
    }
}
