using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Statistics()
        {
            return View("Statistics");
        }
    }
}