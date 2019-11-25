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
    class StockService : CrudQueryServiceBase<Stock, StockDto, StockFilterDto>
    {
        public StockService(IMapper mapper, IRepository<Stock> repository,
            QueryObjectBase<StockDto, Stock, StockFilterDto, IQuery<Stock>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<Stock> GetWithIncludesAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}
