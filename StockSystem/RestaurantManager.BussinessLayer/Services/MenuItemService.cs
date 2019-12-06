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
    public class MenuItemService : CrudQueryServiceBase<MenuItem, MenuItemDto, MenuItemFilterDto>
    {
        public MenuItemService(IMapper mapper, IRepository<MenuItem> repository, QueryObjectBase<MenuItemDto, MenuItem, MenuItemFilterDto, IQuery<MenuItem>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<MenuItem> GetWithIncludesAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}
