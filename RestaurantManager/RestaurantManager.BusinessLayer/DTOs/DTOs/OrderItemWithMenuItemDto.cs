using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class OrderItemWithMenuItemDto : DtoBase
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public MenuItemDto MenuItem { get; set; }
        public bool IsPaid { get; set; }
    }
}
