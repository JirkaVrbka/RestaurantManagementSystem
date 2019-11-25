using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.DAL.Enums;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BusinessLayer.QueryObjects
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
