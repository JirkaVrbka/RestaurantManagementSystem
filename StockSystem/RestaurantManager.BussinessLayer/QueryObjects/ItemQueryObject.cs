using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BusinessLayer.QueryObjects
{
    public class ItemQueryObject : QueryObjectBase<MenuItemDto, StockItem, MenuItemFilterDto, IQuery<StockItem>>
    {
        public ItemQueryObject(IMapper mapper, IQuery<StockItem> query) : base(mapper, query)
        {
        }

        protected override IQuery<StockItem> ApplyWhereClause(IQuery<StockItem> query, MenuItemFilterDto filter)
        {
            if (filter.ItemId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(StockItem.Id), ValueComparingOperator.Equal,
                filter.ItemId));
        }
    }
}
