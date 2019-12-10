using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor.Diagnostics.DebuggerViews;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;
using RestaurantManager.Utils.EntityEnums;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class EmployeesController : Controller
    {
        public EmployeeFacade EmployeeFacade { get; set; }
        public CompanyFacade CompanyFacade { get; set; }

        public async Task<ActionResult> Employees()
        {
            List<EmployeeDto> employees = await CompanyFacade.GetAllEmployees(User.Identity.Name); //E mployeeFacade.GetAllEmployees(User.Identity.Name);
            return View("Employees", employees);
        }

        public ActionResult Create()
        {
            var employee = new EmployeeDto();

            return View(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmployeeDto employee)
        {
            try
            {
                await EmployeeFacade.RegisterEmployee(employee, User.Identity.Name);


                return RedirectToAction("Employees", "Employees");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            await EmployeeFacade.Delete(id);
            return View("Employees");
        }

        public async Task<ActionResult> Edit(int id)
        {
            EmployeeDto employee = await EmployeeFacade.GetAsync(id);

            return View(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Save(EmployeeDto employee)
        {
            try
            {
                await EmployeeFacade.Update(employee);
                return View("Employees");
            }
            catch (Exception)
            {
                return View("Edit");
            }
        }
    }
}