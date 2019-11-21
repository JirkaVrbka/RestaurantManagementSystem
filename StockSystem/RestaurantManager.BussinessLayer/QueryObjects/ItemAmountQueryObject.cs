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
    public class ItemAmountQueryObject : QueryObjectBase<ItemAmountDto, MenuItemAmount, ItemAmountFilterDto, IQuery<MenuItemAmount>>
    {
        public ItemAmountQueryObject(IMapper mapper, IQuery<MenuItemAmount> query) : base(mapper, query)
        {
        }

        protected override IQuery<MenuItemAmount> ApplyWhereClause(IQuery<MenuItemAmount> query, ItemAmountFilterDto filter)
        {
            if (filter.ItemId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(MenuItemAmount.ItemId), ValueComparingOperator.Equal,
                filter.ItemId));
        }
    }
}
