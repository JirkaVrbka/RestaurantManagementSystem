﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DTOs.Filters
{
    public class OrderItemFilterDto : FilterDtoBase
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
    }
}
