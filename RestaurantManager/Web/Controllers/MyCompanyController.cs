using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class MyCompanyController : Controller
    {
        // GET: MyCompany
        
        public ActionResult MyCompany()
        {
            var companyTest = new CompanyDto()
            {
                Name = "Facebook",
                Ico = 12345,
                JoinDate = DateTime.Now
            };
            return View("MyCompany", companyTest);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}