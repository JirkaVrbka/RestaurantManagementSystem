using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    public class EmployeeService : CrudQueryServiceBase<Employee, EmployeeDto, EmployeeFilterDto>
    {
        public EmployeeService(IMapper mapper, IRepository<Employee> repository, QueryObjectBase<EmployeeDto, Employee, EmployeeFilterDto, IQuery<Employee>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<Employee> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, nameof(Employee.Company));
        }

        public async Task<int[]> GetEmployeesIdOfCompany(int companyId)
        {
            var queryResult = await Query.ExecuteQuery(new EmployeeFilterDto { CompanyId = companyId });
            return queryResult.Items.Select(e => e.Id).ToArray();
        }

        public async Task<EmployeeDto> GetEmployeeByEmail(String email)
        {
            var queryResult = await Query.ExecuteQuery(new EmployeeFilterDto { Email = email });
            return queryResult.Items.SingleOrDefault();
        }
    }
}
