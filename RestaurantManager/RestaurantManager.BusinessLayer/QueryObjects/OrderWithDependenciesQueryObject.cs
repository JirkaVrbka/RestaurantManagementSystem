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
    public class OrderWithDependenciesQueryObject : QueryObjectBase<OrderWithFullDependencyDto, Order, OrderFilterDto, IQuery<Order>>
    {
        public OrderWithDependenciesQueryObject(IMapper mapper, IQuery<Order> query) : base(mapper, query)
        {
        }

        protected override IQuery<Order> ApplyWhereClause(IQuery<Order> query, OrderFilterDto filter)
        {
            return filter.CompanyId == 0
                ? query
                : query.Where(new SimplePredicate(nameof(Order.CompanyId), ValueComparingOperator.Equal,
                    filter.CompanyId));
        }
    }
}
