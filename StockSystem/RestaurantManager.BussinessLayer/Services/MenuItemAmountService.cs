using System;
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
    public class MenuItemAmountService : CrudQueryServiceBase<MenuItemAmount, MenuItemAmountDto, MenuItemAmountFilterDto>
    {
        public MenuItemAmountService(IMapper mapper, IRepository<MenuItemAmount> repository, QueryObjectBase<MenuItemAmountDto, MenuItemAmount, MenuItemAmountFilterDto, IQuery<MenuItemAmount>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<MenuItemAmount> GetWithIncludesAsync(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
