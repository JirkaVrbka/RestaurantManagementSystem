using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Windsor;
using RestaurantManager.BusinessLayer.Config;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EF;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    public class OrderService : CrudQueryServiceBase<Order, OrderDto, OrderFilterDto>
    {
        private QueryObjectBase<OrderDto, Order, OrderClosedFilterDto, IQuery<Order>> queryIsClosed;

        public OrderService(IMapper mapper,
            IRepository<Order> repository,
            QueryObjectBase<OrderDto, Order, OrderFilterDto, IQuery<Order>> query,
            QueryObjectBase<OrderDto, Order, OrderClosedFilterDto, IQuery<Order>> queryIsClosed)
            : base(mapper, repository, query)
        {
            this.queryIsClosed = queryIsClosed;
        }

        protected override Task<Order> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, new string[] { nameof(Order.Company) });
        }

        public async Task<List<OrderDto>> GetOrderOfCompany(int companyId)
        {
            var queryResult = await Query.ExecuteQuery(new OrderFilterDto { CompanyId = companyId });
            return queryResult.Items.ToList();
        }

        public async Task<List<OrderWithFullDependencyDto>> GetOrderOfCompanyWithOrderItems(int companyId)
        {
            var queryResult = await Query.ExecuteQuery(new OrderFilterDto { CompanyId = companyId });
            List<OrderWithFullDependencyDto> orders = new List<OrderWithFullDependencyDto>();
            foreach (var result in queryResult.Items)
            {
                orders.Add(Mapper.Map<OrderWithFullDependencyDto>(Repository.GetAsync(result.Id, nameof(Order.Items)).Result));
            }

            return orders;
        }

        public async Task<List<OrderWithFullDependencyDto>> GetUnclosedOrdersOfCompany(int companyId)
        {
            var queryResult = await queryIsClosed.ExecuteQuery(new OrderClosedFilterDto { CompanyId = companyId, IsClosed = false});
            List<OrderWithFullDependencyDto> orders = new List<OrderWithFullDependencyDto>();
            foreach (var result in queryResult.Items)
            {
                orders.Add(Mapper.Map<OrderWithFullDependencyDto>(await Repository.GetAsync(result.Id, nameof(Order.Items))));
            }

            return orders;
        }

    }
}
