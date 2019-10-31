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
    public class ItemAmountQueryObject : QueryObjectBase<ItemAmountDto, ItemAmount, ItemAmountFilterDto, IQuery<ItemAmount>>
    {
        public ItemAmountQueryObject(IMapper mapper, IQuery<ItemAmount> query) : base(mapper, query)
        {
        }

        protected override IQuery<ItemAmount> ApplyWhereClause(IQuery<ItemAmount> query, ItemAmountFilterDto filter)
        {
            if (filter.ItemId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(ItemAmount.ItemId), ValueComparingOperator.Equal,
                filter.ItemId));
        }
    }
}
