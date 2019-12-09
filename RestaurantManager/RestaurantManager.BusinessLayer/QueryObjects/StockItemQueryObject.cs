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
    public class StockItemQueryObject : QueryObjectBase<StockItemDto, StockItem, StockItemFilterDto, IQuery<StockItem>>
    {
        public StockItemQueryObject(IMapper mapper, IQuery<StockItem> query) : base(mapper, query)
        {
        }

        protected override IQuery<StockItem> ApplyWhereClause(IQuery<StockItem> query, StockItemFilterDto filter)
        {
            return filter.CompanyId == 0
                ? query
                : query.Where(new SimplePredicate(nameof(StockItem.CompanyId), ValueComparingOperator.Equal,
                    filter.CompanyId));
        }
    }
}
