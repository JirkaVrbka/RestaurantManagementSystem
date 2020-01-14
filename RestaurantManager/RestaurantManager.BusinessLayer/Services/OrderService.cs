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

        public OrderService(IMapper mapper, 
            IRepository<Order> repository,
            QueryObjectBase<OrderDto, Order, OrderFilterDto, IQuery<Order>> query)
            : base(mapper, repository, query)
        {}

        protected override Task<Order> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, new string[] { nameof(Order.Company) });
        }

        public async Task<List<OrderDto>> GetStockItemsOfCompany(int companyId)
        {
            var queryResult = await Query.ExecuteQuery(new OrderFilterDto { CompanyId = companyId });
            return queryResult.Items.ToList();
        }
    }
}
