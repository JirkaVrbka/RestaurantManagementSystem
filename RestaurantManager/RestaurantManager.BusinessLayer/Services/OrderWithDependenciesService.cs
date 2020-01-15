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
    public class OrderWithDependenciesService : CrudQueryServiceBase<Order, OrderWithFullDependencyDto, OrderFilterDto>
    {
        public OrderWithDependenciesService(
            IMapper mapper, 
            IRepository<Order> repository,
            QueryObjectBase<OrderWithFullDependencyDto, Order, OrderFilterDto, IQuery<Order>> query
            ) : base(mapper,repository, query)
        {
        }

        protected override Task<Order> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, new string[] { nameof(Order.Company) });
        }

        public async Task<List<OrderWithFullDependencyDto>> GetStockItemsOfCompanyWithDependencies(int companyId)
        {
            
            var queryResult = await Query.ExecuteQuery(new OrderFilterDto { CompanyId = companyId });
            return queryResult.Items.ToList();
        }

        public async Task<OrderWithFullDependencyDto> GetAsyncWithOrderItems(int entityId)
        {
            Order order = await Repository.GetAsync(entityId, new string[] { nameof(Order.Items) });
            return Mapper.Map<Order, OrderWithFullDependencyDto>(order);
        }
    }
}
