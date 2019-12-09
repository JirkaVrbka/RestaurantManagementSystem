using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;

namespace Web.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult MyAccount()
        {
            var accountTest = new EmployeeDto()
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "jsmith@email.com"
            };
            return View("MyAccount", accountTest);
        }
    }
}