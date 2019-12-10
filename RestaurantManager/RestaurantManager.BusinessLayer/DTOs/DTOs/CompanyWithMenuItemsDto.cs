using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class CompanyWithMenuItemsDto : CompanyDto
    {
        public virtual List<MenuItemDto> MenuItems { get; set; }
    }
}
