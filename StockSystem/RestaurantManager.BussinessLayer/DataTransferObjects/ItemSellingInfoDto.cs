using RestaurantManager.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects
{
    class ItemSellingInfoDto : DtoBase
    {

        public string Name { get; set; }
        public int SellPrice { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}
