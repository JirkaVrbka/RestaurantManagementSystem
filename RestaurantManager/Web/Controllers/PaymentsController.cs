using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class PaymentsController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }
        // GET: Payments
        public async Task<ActionResult> Payments()
        {
            var payments = await CompanyFacade.GetAllPayments(User.Identity.Name);

            return View("Payments", payments);
        }
    }
}