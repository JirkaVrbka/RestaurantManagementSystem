using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class PaymentsController : Controller
    {
        // GET: Payments
        public ActionResult Payments()
        {
            var paymentsTest = new List<PaymentDto>()
            {
                new PaymentDto()
                {
                    Amount = 800,

                    DueDate = new DateTime(2019, 12, 10),
                    DateOfPayment = new DateTime(2019, 12, 1),
                    ReceivedAmount = 800,
                    VariableNumber = "asd"
                },
            };

            return View("Payments", paymentsTest);
        }
    }
}