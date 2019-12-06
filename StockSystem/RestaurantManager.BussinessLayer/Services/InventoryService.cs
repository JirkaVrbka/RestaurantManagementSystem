using System;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    public class InventoryService: CrudQueryServiceBase<Inventory, InventoryDto, InventoryFilterDto>
    {
        public InventoryService(IMapper mapper, IRepository<Inventory> repository, QueryObjectBase<InventoryDto, Inventory, InventoryFilterDto, IQuery<Inventory>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<Inventory> GetWithIncludesAsync(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
