using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.ClientServices;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.Facades;
using RestaurantManager.DAL.Enums;
using Web.Models;

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

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            //(bool success, var person) = await _personFacade.Login(model.Email, model.Password);
            var success = true;
             var person = new PersonCreateDto()
             {
                 Id = 2,
                 Role = Role.Owner
             };

            if (success)
            {
                Session["Id"] = person.Id;
                var authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, person.Role.ToString());
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                var x = User.IsInRole("Owner");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Wrong username or password!");
            return View();
        }

        public ActionResult RedirectToRegister()
        {
            return RedirectToAction("Register");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(CustomerCreateDto customer)
        {
            await _personFacade.RegisterCustomer(customer);
            return RedirectToAction("Index", "Owner");
        }
    }
}