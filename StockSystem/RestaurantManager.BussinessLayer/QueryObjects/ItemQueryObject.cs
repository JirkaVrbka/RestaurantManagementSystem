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
    public class ItemQueryObject : QueryObjectBase<ItemDto, Item, ItemFilterDto, IQuery<Item>>
    {
        public ItemQueryObject(IMapper mapper, IQuery<Item> query) : base(mapper, query)
        {
        }

        protected override IQuery<Item> ApplyWhereClause(IQuery<Item> query, ItemFilterDto filter)
        {
            if (filter.ItemId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Item.Id), ValueComparingOperator.Equal,
                filter.ItemId));
        }
    }
}
