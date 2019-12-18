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
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;
using RestaurantManager.Utils.EntityEnums;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }
        public EmployeeFacade EmployeeFacade { get; set; }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            (bool success, string role) = await EmployeeFacade.Login(model.Email, model.Password);
            
            if (success)
            {
                //FormsAuthentication.SetAuthCookie(model.Username, false);
                /*
                var authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, role);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);*/

                AddAuthTicket(model.Email, role);
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
        public async Task<ActionResult> Register(NewCustomerDto customer)
        {
                var isIcoAlreadyRegistered = (await CompanyFacade.GetAsyncByIco(customer.Ico)) != null;
                var isEmailAlreadyRegistered = (await EmployeeFacade.GetAsyncByEmail(customer.Email)) != null;

                if (isIcoAlreadyRegistered)
                {
                    ModelState.AddModelError("Ico", "Ico is already registered");
                    return View();
                }

                if (isEmailAlreadyRegistered)
                {
                    ModelState.AddModelError("Username", "Account with that username already exists!");
                    return View();
                }
                

                await CompanyFacade.RegisterCompanyWithOwner(customer);
                AddAuthTicket(customer.Email, Role.Owner.ToString());
            /*
                var authTicket = new FormsAuthenticationTicket(1, customer.Email, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "Owner");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);*/

                return RedirectToAction("Index", "Home");
        }

        private void AddAuthTicket(String email, String role)
        {
            var authTicket = new FormsAuthenticationTicket(1, email, DateTime.Now,
                DateTime.Now.AddMinutes(30), false, role);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
        }


        //public ActionResult RedirectToRegisterCompany()
        //{
        //    return View("RegisterCompany");
        //}

        //[HttpPost]
        //public async Task<ActionResult> RegisterCompany(CompanyDto company)
        //{
        //    company.JoinDate = DateTime.Now;
        //    await CompanyFacade.RegisterCompany(company);
        //    return RedirectToAction("Login");
        //}
    }
}