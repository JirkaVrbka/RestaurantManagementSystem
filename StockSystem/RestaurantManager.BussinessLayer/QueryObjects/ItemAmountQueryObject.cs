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
    public class ItemAmountQueryObject : QueryObjectBase<MenuItemAmountDto, MenuItemAmount, MenuItemAmountFilterDto, IQuery<MenuItemAmount>>
    {
        public ItemAmountQueryObject(IMapper mapper, IQuery<MenuItemAmount> query) : base(mapper, query)
        {
        }

        protected override IQuery<MenuItemAmount> ApplyWhereClause(IQuery<MenuItemAmount> query, MenuItemAmountFilterDto filter)
        {
            if (filter.ItemId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(MenuItemAmount.MenuItemId), ValueComparingOperator.Equal,
                filter.ItemId));
        }
    }
}
