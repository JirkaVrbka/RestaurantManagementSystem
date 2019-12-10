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
    public class MenuItemService : CrudQueryServiceBase<MenuItem, MenuItemDto, MenuItemFilterDto>
    {
        public MenuItemService(IMapper mapper, IRepository<MenuItem> repository, QueryObjectBase<MenuItemDto, MenuItem, MenuItemFilterDto, IQuery<MenuItem>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<MenuItem> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, new string[] { nameof(MenuItem.Company), nameof(MenuItem.InStockItem)});
        }
    }
}
