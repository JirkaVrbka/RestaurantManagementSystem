using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects
{
    class PaymentDto : DtoBase
    {
        public DateTime? DateOfPayment { get; set; }
        public int Amount { get; set; }
    }
}
