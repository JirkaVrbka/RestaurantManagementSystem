using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantManager.BusinessLayer.DTOs.DTOs;

namespace Web.Models
{
    public class OrderDetail
    {
        public List<MenuItemDto> menuItems { get; set; }
        public OrderWithFullDependencyDto order { get; set; }
    }
}