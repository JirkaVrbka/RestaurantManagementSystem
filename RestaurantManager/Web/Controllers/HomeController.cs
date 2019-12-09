using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.Facades;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }

        public ActionResult Index()
        {
            if (CompanyFacade == null)
            {
                Console.WriteLine("Yajks");
                CompanyFacade =  MvcApplication.Container.Resolve<CompanyFacade>();
                if (CompanyFacade == null)
                {
                    Console.WriteLine("aha");
                }
                else
                {
                    Console.WriteLine("aha!");
                }
            }
            else
            {
                Console.WriteLine("Uiiii!");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}