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
    public class MenuItemQueryObject : QueryObjectBase<MenuItemDto, MenuItem, MenuItemFilterDto, IQuery<MenuItem>>
    {
        public MenuItemQueryObject(IMapper mapper, IQuery<MenuItem> query) : base(mapper, query)
        {
        }

        protected override IQuery<MenuItem> ApplyWhereClause(IQuery<MenuItem> query, MenuItemFilterDto filter)
        {
            return filter.CompanyId == 0
                ? query
                : query.Where(new SimplePredicate(nameof(MenuItem.CompanyId), ValueComparingOperator.Equal,
                    filter.CompanyId));
        }
    }
}
