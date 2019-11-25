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
    public class ItemQueryObject : QueryObjectBase<ItemDto, StockItem, ItemFilterDto, IQuery<StockItem>>
    {
        public ItemQueryObject(IMapper mapper, IQuery<StockItem> query) : base(mapper, query)
        {
        }

        protected override IQuery<StockItem> ApplyWhereClause(IQuery<StockItem> query, ItemFilterDto filter)
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
