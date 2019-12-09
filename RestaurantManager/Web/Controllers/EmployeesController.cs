using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.Utils.EntityEnums;

namespace Web.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
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
    }
}