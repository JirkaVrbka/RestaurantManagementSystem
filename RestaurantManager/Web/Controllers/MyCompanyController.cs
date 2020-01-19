using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class MyCompanyController : Controller
    {
        // GET: MyCompany
        public CompanyFacade CompanyFacade { get; set; }
        
        public async Task<ActionResult> MyCompany()
        {
            CompanyDto company = await CompanyFacade.FindCompanyByEmployeeEmail(User.Identity.Name);
            return View("MyCompany", company);
        }

        public async Task<ActionResult> Edit(int id)
        {
            CompanyDto company = await CompanyFacade.GetAsync(id);
            return View(new CompanyUpdateNameDto{Id = company.Id, Name = company.Name});
        }

        [HttpPost]
        public async Task<ActionResult> Save(CompanyUpdateNameDto companyUpdate)
        {

            CompanyDto company = await CompanyFacade.GetAsync(companyUpdate.Id);
            try
            {
                await CompanyFacade.Update(companyUpdate);
                company.Name = companyUpdate.Name;
                return View("MyCompany", company);
            }
            catch(Exception)
            {
                return View("Edit", new CompanyUpdateNameDto { Id = company.Id, Name = company.Name });
            }
        }
    }
}