using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantManager.BusinessLayer.DTOs.DTOs;

namespace Web.Models
{
    public class NewItemToOrder
    {
        public List<MenuItemDto> Items { get; set; }
        public int SelectedItem { get; set; }
        public int OrderId { get; set; }
    }
}