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
    public class OrderItemQueryObject : QueryObjectBase<OrderItemDto, OrderItem, OrderItemFilterDto, IQuery<OrderItem>>
    {
        public OrderItemQueryObject(IMapper mapper, IQuery<OrderItem> query) : base(mapper, query)
        {
        }

        protected override IQuery<OrderItem> ApplyWhereClause(IQuery<OrderItem> query, OrderItemFilterDto filter)
        {
            if (filter.MenuItemId <= 0)
            {
                if (filter.OrderId <= 0)
                {
                    return query;
                }
                else
                {
                    return query.Where(new SimplePredicate(nameof(OrderItem.OrderId), ValueComparingOperator.Equal,
                        filter.OrderId));
                }
            }
            else
            {
                if (filter.OrderId <= 0)
                {
                    return query.Where(new SimplePredicate(nameof(OrderItem.MenuItemId), ValueComparingOperator.Equal,
                        filter.MenuItemId));
                }
                else
                {
                    return query.Where(
                        new CompositePredicate(new List<IPredicate>()
                        {
                            new SimplePredicate(nameof(OrderItem.OrderId), ValueComparingOperator.Equal, filter.OrderId),
                            new SimplePredicate(nameof(OrderItem.MenuItemId), ValueComparingOperator.Equal, filter.MenuItemId)
                        }, LogicalOperator.AND)
                    );
                }
            }
        }
    }
}
