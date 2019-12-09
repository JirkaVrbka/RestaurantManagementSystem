using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var employeesTest = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Role = Role.Manager,
                    Email = "someEmail@email.com",
                    FirstName = "Carl",
                    LastName = "Newman"
                }
            };
            return View("Employees", employeesTest);
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
                await EmployeeFacade.RegisterEmployee(employee);


                return RedirectToAction("Employees", "Employees");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }
    }
}