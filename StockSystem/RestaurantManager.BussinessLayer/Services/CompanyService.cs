using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    public class CompanyService : CrudQueryServiceBase<Company, CompanyDto, CompanyFilterDto>
    {
        public CompanyService(IMapper mapper, IRepository<Company> repository, QueryObjectBase<CompanyDto, Company, CompanyFilterDto, IQuery<Company>> query) : base(mapper, repository, query)
        {
        }

        public async Task<CompanyDto> GetByIco(int ico)
        {
            var result = await Query.ExecuteQuery(new CompanyFilterDto() { Ico = ico });
            return result.Items.SingleOrDefault();
        }

        public async Task<int> RegisterCompanyAsync(CompanyCreateDto companyCreateDto)
        {
            var company = Mapper.Map<Company>(companyCreateDto);

            if (await GetIfCompanyExistsAsync(company.Ico))
            {
                throw new ArgumentException("Company with this Ico already exists!");
            }

            Repository.Create(company);

            return company.Id;
        }

        private async Task<bool> GetIfCompanyExistsAsync(int ico)
        {
            var queryResult = await Query.ExecuteQuery(new CompanyFilterDto() { Ico = ico });
            return (queryResult.Items.Count() == 1);
        }


        protected override async Task<Company> GetWithIncludesAsync(int entityId)
        {

            return await Repository.GetAsync(entityId, new string[] { "People", "MenuItems", "Inventories", "Payments"});
        }

        public async Task<CompanyWithPeopleDto> GetWithIncludesPeopleAsync(int entityId)
        {
            var company = await Repository.GetAsync(entityId, new string[] { "People" });
            return Mapper.Map<CompanyWithPeopleDto>(company);
        }
    }
}
