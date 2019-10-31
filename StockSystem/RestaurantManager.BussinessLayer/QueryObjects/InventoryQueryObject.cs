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
