﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects.Filters
{
    public class PaymentFilterDto : FilterDtoBase
    {
        public int PaymentId { get; set; }
    }
}