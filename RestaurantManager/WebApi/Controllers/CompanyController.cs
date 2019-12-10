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
    public class CompanyController : ApiController
    {
        public CompanyFacade CompanyFacade { get; set; }
        // GET: api/Company
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Company/5
        [HttpGet, Route("api/Company/Get")]
        public async Task<CompanyDto> Get(int id)
        {
            return await CompanyFacade.GetAsync(id);
        }

        // POST: api/Company/Create
        [HttpPost, Route("api/Company/Create")]
        public async Task Post([FromBody]CompanyDto value)
        {
            await CompanyFacade.Create(value);
        }

        // PUT: api/Company/5
        [HttpPost, Route("api/Company/Put")]
        public async Task Put(int id, [FromBody]CompanyUpdateNameDto nameDto)
        {
            await CompanyFacade.Update(nameDto);
        }

        [HttpGet, Route("api/Company/Delete")]
        // DELETE: api/Company/5
        public async Task Delete(int id)
        {
            await CompanyFacade.Delete(id);
        }
    }
}
