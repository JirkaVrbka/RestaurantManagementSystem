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
    class OrderService : CrudQueryServiceBase<Order, OrderDto, OrderFilterDto>
    {
        public OrderService(IMapper mapper, IRepository<Order> repository, QueryObjectBase<OrderDto, Order, OrderFilterDto, IQuery<Order>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<Order> GetWithIncludesAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}
