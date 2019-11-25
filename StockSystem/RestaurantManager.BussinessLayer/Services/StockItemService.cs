using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    class StockItemService : CrudQueryServiceBase<StockItem, StockItemDto, StockItemFilterDto>
    {
        public StockItemService(IMapper mapper, IRepository<StockItem> repository, QueryObjectBase<StockItemDto, StockItem, StockItemFilterDto, IQuery<StockItem>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<StockItem> GetWithIncludesAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}
