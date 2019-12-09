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
    public class MyCompanyController : Controller
    {
        // GET: MyCompany
        public CompanyFacade CompanyFacade { get; set; }
        
        public ActionResult MyCompany()
        {
            CompanyDto company = CompanyFacade.GetCompany(User.Identity.Name);
            return View("MyCompany", company);
        }

        public async Task<ActionResult> Edit(int id)
        {
            CompanyDto company = await CompanyFacade.GetAsync(id);
            return View(company);
        }

        [HttpPost]
        public ActionResult Save(CompanyDto company)
        {
            try
            {
                CompanyFacade.Update(company);
                return View("MyCompany");
            }
            catch(Exception)
            {
                return View("Edit");
            }
        }
    }
}