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

        public ActionResult Employees()
        {
            List<EmployeeDto> employees = EmployeeFacade.GetAllEmplayes(User.Identity.Name);
            return View("Employees", employees);
        }

        public ActionResult Create()
        {
            var employee = new EmployeeDto();

            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(EmployeeDto employee)
        {
            try
            {
                EmployeeFacade.RegisterEmployee(employee, User.Identity.Name);


                return RedirectToAction("Employees", "Employees");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            EmployeeFacade.Delete(id);
            return View("Employees");
        }

        public async Task<ActionResult> Edit(int id)
        {
            EmployeeDto employee = await EmployeeFacade.GetAsync(id);

            return View(employee);
        }

        [HttpPost]
        public ActionResult Save(EmployeeDto employee)
        {
            try
            {
                EmployeeFacade.Update(employee);
                return View("Employees");
            }
            catch (Exception)
            {
                return View("Edit");
            }
        }
    }
}