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
using RestaurantManager.BusinessLayer.DTOs;
using RestaurantManager.BusinessLayer.Facades;
using RestaurantManager.Utils.EntityEnums;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }


        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        { 

            //if (success)
            //{
            //    //Session["Id"] = person.Id;
            //    //Session["Role"] = person.Role.ToString();
            //    //Console.WriteLine(person.Role.ToString());

            //    var authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now,
            //        DateTime.Now.AddMinutes(30), false, person.Role.ToString());
            //    System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
            //    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //    HttpContext.Response.Cookies.Add(authCookie);

            //    return RedirectToAction("Index", "Home");
            //}

            //ModelState.AddModelError("", "Wrong username or password!");
            return View("Login");
        }

        public ActionResult RedirectToRegister()
        {
            return RedirectToAction("Register");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<ActionResult> Register(EmployeeDto customer)
        {
            return RedirectToAction("Index", "Home");
        }


        public ActionResult RedirectToRegisterCompany()
        {
            return View("RegisterCompany");
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCompany(CompanyDto company)
        {
            company.JoinDate = DateTime.Now;
            await CompanyFacade.RegisterCompany(company);
            return RedirectToAction("Login");
        }
    }
}