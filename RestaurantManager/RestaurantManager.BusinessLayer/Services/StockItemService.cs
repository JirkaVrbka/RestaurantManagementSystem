using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    public class StockItemService : CrudQueryServiceBase<StockItem, StockItemDto, StockItemFilterDto>
    {
        public StockItemService(IMapper mapper, IRepository<StockItem> repository, QueryObjectBase<StockItemDto, StockItem, StockItemFilterDto, IQuery<StockItem>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<StockItem> GetWithIncludesAsync(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
