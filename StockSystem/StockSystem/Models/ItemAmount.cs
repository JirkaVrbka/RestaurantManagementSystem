﻿using System;
using System.Collections.Generic;
using System.Text;
using RestaurantManager.DAL.Enums;

namespace DAL.Models
{
    public class ItemAmount
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int Amount { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
