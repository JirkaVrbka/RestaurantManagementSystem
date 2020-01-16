using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DTOs;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.BusinessLayer.QueryObjects;
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

        public async Task RegisterCompanyAsync(CompanyDto companyCreateDto)
        {
            var company = Mapper.Map<Company>(companyCreateDto);

            if (await IsCompanyAlreadyInDb(company.Ico))
            {
                throw new ArgumentException("Company with this Ico already exists!");
            }

            Repository.Create(company);
        }

        private async Task<bool> IsCompanyAlreadyInDb(int ico)
        {
            var queryResult = await Query.ExecuteQuery(new CompanyFilterDto() { Ico = ico });
            return (queryResult.Items.Count() >= 1);
        }

        protected override async Task<Company> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, new string[] { nameof(CompanyWithIncludesDto.Employees), nameof(CompanyWithIncludesDto.MenuItems), nameof(CompanyWithIncludesDto.Orders), nameof(CompanyWithIncludesDto.Payments), nameof(CompanyWithIncludesDto.Stock) });
        }

        public async Task<CompanyWithEmployeesDto> GetAsyncWithEmployees(int entityId)
        {
            Company company =  await Repository.GetAsync(entityId, new string[] { nameof(CompanyWithIncludesDto.Employees)});
            return Mapper.Map<Company, CompanyWithEmployeesDto>(company);
        }

        public async Task<CompanyWithMenuItemsDto> GetAsyncWithMenuItems(int entityId)
        {
            Company company = await Repository.GetAsync(entityId, new string[] { nameof(Company.MenuItems) });
            return Mapper.Map<Company, CompanyWithMenuItemsDto>(company);
        }

        public async Task<CompanyWithOrdersDto> GetAsyncWithOrders(int entityId)
        {
            Company company = await Repository.GetAsync(entityId, new string[] { nameof(Company.Orders) });
            return Mapper.Map<Company, CompanyWithOrdersDto>(company);
        }

        public async Task<CompanyWithPaymentsDto> GetAsyncWithPayments(int entityId)
        {
            Company company = await Repository.GetAsync(entityId, new string[] { nameof(Company.Payments) });
            return Mapper.Map<Company, CompanyWithPaymentsDto>(company);
        }
    }
}
