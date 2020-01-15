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
    public class OrderItemByOrderQueryObject : QueryObjectBase<OrderItemDto, OrderItem, OrderItemFilterByOrderDto, IQuery<OrderItem>>
    {
        public OrderItemByOrderQueryObject(IMapper mapper, IQuery<OrderItem> query) : base(mapper, query)
        {
        }

        protected override IQuery<OrderItem> ApplyWhereClause(IQuery<OrderItem> query, OrderItemFilterByOrderDto filter)
        {
            return  query.Where(new SimplePredicate(nameof(OrderItem.OrderId), ValueComparingOperator.Equal,
                    filter.OrderId));
        }

    }
}
