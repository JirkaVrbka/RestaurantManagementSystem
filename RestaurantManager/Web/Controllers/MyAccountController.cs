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
    [Authorize]
    public class MyAccountController : Controller
    {
        public EmployeeFacade EmployeeFacade { get; set; }
        
        public async Task<ActionResult> MyAccount()
        {
            var employee = await EmployeeFacade.GetAsyncByEmail(User.Identity.Name);
            return View("MyAccount", employee);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var employee = await EmployeeFacade.GetAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Save(EmployeeDto employee)
        {
            try
            {
                await EmployeeFacade.Update(employee);
                return View("MyAccount");
            }
            catch (Exception)
            {
                return View("Edit");
            }
        }
    }
}