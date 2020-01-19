using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
                    ModelState.AddModelError("Email", "Account with that email already exists!");
                    return View();
                }
                

                await CompanyFacade.RegisterCompanyWithOwner(customer);
                AddAuthTicket(customer.Email, Role.Owner.ToString());

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
    }
}