using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BussinessLayer.DataTransferObjects;using RestaurantManager.BussinessLayer.DataTransferObjects.Filters;using RestaurantManager.BussinessLayer.QueryObjects.Common;using RestaurantManager.DAL.Models;using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BussinessLayer.QueryObjects
{
    public class RoleQueryObject : QueryObjectBase<RoleCreateDto, Role, RoleFilterDto, IQuery<Role>>
    {
        public RoleQueryObject(IMapper mapper, IQuery<Role> query) : base(mapper, query)
        {
        }

        protected override IQuery<Role> ApplyWhereClause(IQuery<Role> query, RoleFilterDto filter)
        {
            if (string.IsNullOrEmpty(filter.Name))
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Role.Name), ValueComparingOperator.Equal,
                filter.Name));
        }
    }
}
