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
    public class OrderItemService : CrudQueryServiceBase<OrderItem, OrderItemDto, OrderItemFilterDto>
    {
        private QueryObjectBase<OrderItemDto, OrderItem, OrderItemFilterByOrderDto, IQuery<OrderItem>> QueryByOrder;
        private QueryObjectBase<OrderItemWithMenuItemDto, OrderItem, OrderItemFilterByOrderDto, IQuery<OrderItem>> QueryWithMenuItemByOrder;
        public OrderItemService(
            IMapper mapper, 
            IRepository<OrderItem> repository, 
            QueryObjectBase<OrderItemDto, OrderItem, OrderItemFilterDto, IQuery<OrderItem>> query,
            QueryObjectBase<OrderItemDto, OrderItem, OrderItemFilterByOrderDto, IQuery<OrderItem>> queryByOrder,
            QueryObjectBase<OrderItemWithMenuItemDto, OrderItem, OrderItemFilterByOrderDto, IQuery<OrderItem>> queryWithMenuItemByOrder
            ) : base(mapper, repository, query)
        {
            QueryByOrder = queryByOrder;
            QueryWithMenuItemByOrder = queryWithMenuItemByOrder;
        }

        protected override Task<OrderItem> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, new string[] { nameof(OrderItem.MenuItem), nameof(OrderItem.Order) });
        }

        public async Task<List<OrderItemDto>> GetByOrderId(int id)
        {
            var queryResult = await QueryByOrder.ExecuteQuery(new OrderItemFilterByOrderDto { OrderId = id });
            return queryResult.Items.ToList();
        }

        public async Task<List<OrderItemWithMenuItemDto>> GetWithMenuItemByOrderId(int id)
        {
            var queryResult = await QueryWithMenuItemByOrder.ExecuteQuery(new OrderItemFilterByOrderDto { OrderId = id });
            return queryResult == null ? new List<OrderItemWithMenuItemDto>() : queryResult.Items.ToList();
        }
    }
}
