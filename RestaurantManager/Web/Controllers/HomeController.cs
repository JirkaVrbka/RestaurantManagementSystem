﻿using System.Web.Mvc;

namespace Web.Controllers
{
    
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            return View("About");
        }

        
    }
}