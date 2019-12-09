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
    public class OrderService : CrudQueryServiceBase<Order, OrderDto, OrderFilterDto>
    {
        public OrderService(IMapper mapper, IRepository<Order> repository, QueryObjectBase<OrderDto, Order, OrderFilterDto, IQuery<Order>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<Order> GetWithIncludesAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDto>> GetStockItemsIdOfCompany(int companyId)
        {
            var queryResult = await Query.ExecuteQuery(new OrderFilterDto { CompanyId = companyId });
            return queryResult.Items.ToList();
        }
    }
}
