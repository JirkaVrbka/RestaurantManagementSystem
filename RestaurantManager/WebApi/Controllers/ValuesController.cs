using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        public CompanyFacade CompanyFacade { get; set; }

        public EmployeeFacade EmployeeFacade { get; set; }

        public MenuItemFacade MenuItemFacade { get; set; }


        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet, Route("api/Values/Company")]
        public async Task<CompanyDto> GetCompany(int id)
        {
            return await CompanyFacade.GetAsync(id);
        }

        [HttpGet, Route("api/Values/Employee")]
        public async Task<EmployeeDto> GetEmployee(int id)
        {
            return await EmployeeFacade.GetAsync(id);
        }

        [HttpGet, Route("api/Values/Employee")]
        public async Task<EmployeeDto> GetEmployee(string email)
        {
            return await EmployeeFacade.GetAsyncByEmail(email);
        }

        [HttpGet, Route("api/Values/MenuItem")]
        public async Task<MenuItemDto> GetMenuItem(int id)
        {
            return await MenuItemFacade.GetAsync(id);
        }
    }
}
