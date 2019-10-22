using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.DataTransferObjects
{
    class PaymentInfoDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public List<PaymentDto> Payments { get; set; }
        public int CompanyId { get; set; }
    }
}
