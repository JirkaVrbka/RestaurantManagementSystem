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
    public class StockQueryObject : QueryObjectBase<StockDto, Stock, StockFilterDto, IQuery<Stock>>
    {
        public StockQueryObject(IMapper mapper, IQuery<Stock> query) : base(mapper, query)
        {
        }

        protected override IQuery<Stock> ApplyWhereClause(IQuery<Stock> query, StockFilterDto filter)
        {
            if (filter.StockId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Stock.Id), ValueComparingOperator.Equal,
                filter.StockId));
        }
    }
}
