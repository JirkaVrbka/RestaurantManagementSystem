﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BusinessLayer.DataTransferObjects.Dtos
{
    public class CompanyCreateDto : DtoBase
    {
        public string Name { get; set; }
        public int Ico { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}