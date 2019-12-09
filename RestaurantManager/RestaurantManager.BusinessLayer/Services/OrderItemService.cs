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
        public OrderItemService(IMapper mapper, IRepository<OrderItem> repository, QueryObjectBase<OrderItemDto, OrderItem, OrderItemFilterDto, IQuery<OrderItem>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<OrderItem> GetWithIncludesAsync(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
