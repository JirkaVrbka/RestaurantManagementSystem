using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BussinessLayer.DataTransferObjects;
using RestaurantManager.BussinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BussinessLayer.QueryObjects.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BussinessLayer.QueryObjects
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
