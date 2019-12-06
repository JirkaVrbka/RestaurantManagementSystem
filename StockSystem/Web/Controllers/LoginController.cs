using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.Facades;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private PersonFacade _personFacade;

        public LoginController(PersonFacade personFacade)
        {
            _personFacade = personFacade;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public async Task<ActionResult> Register(PersonCreateDto person)
        {
            await _personFacade.RegisterCustomer(person);
            return RedirectToAction("Index", "Owner");
        }
    }
}