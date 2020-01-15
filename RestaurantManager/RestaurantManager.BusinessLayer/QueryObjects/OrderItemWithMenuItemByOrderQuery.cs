using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BusinessLayer.QueryObjects
{
    public class OrderItemWithMenuItemByOrderQuery : QueryObjectBase<OrderItemWithMenuItemDto, OrderItem, OrderItemFilterByOrderDto, IQuery<OrderItem>>
    {
        public OrderItemWithMenuItemByOrderQuery(IMapper mapper, IQuery<OrderItem> query) : base(mapper, query)
        {
        }

        protected override IQuery<OrderItem> ApplyWhereClause(IQuery<OrderItem> query, OrderItemFilterByOrderDto filter)
        {
            return query.Where(new SimplePredicate(nameof(OrderItem.OrderId), ValueComparingOperator.Equal,
                filter.OrderId));
        }

    }
}
