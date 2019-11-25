using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BusinessLayer.QueryObjects
{
    public class InventoryQueryObject : QueryObjectBase<InventoryDto, Inventory, InventoryFilterDto, IQuery<Inventory>>
    {
        public InventoryQueryObject(IMapper mapper, IQuery<Inventory> query) : base(mapper, query)
        {
        }

        protected override IQuery<Inventory> ApplyWhereClause(IQuery<Inventory> query, InventoryFilterDto filter)
        {
            if (filter.CompanyId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Inventory.CompanyId), ValueComparingOperator.Equal,
                filter.CompanyId));
        }
    }
}
