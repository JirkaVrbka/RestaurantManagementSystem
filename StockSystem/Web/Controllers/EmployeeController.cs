using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }

        public ActionResult MyAccount()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}