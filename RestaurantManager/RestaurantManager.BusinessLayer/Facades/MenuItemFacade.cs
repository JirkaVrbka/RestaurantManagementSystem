using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades.Common;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.BusinessLayer.Facades
{
    public class MenuItemFacade : FacadeBase
    {
        private MenuItemService _menuItemService;
        public MenuItemFacade(IUnitOfWorkProvider unitOfWorkProvider, MenuItemService menuItemService) : base(unitOfWorkProvider)
        {
            _menuItemService = menuItemService;
        }


        public async Task Create(MenuItemDto menuItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                _menuItemService.Create(menuItem);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(MenuItemDto menuItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                _menuItemService.DeleteProduct(menuItem.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int menuItemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _menuItemService.DeleteProduct(menuItemId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(MenuItemDto menuItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _menuItemService.Update(menuItem);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<MenuItemDto> GetAsync(int menuItemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _menuItemService.GetAsync(menuItemId, false);
            }
        }
    }

}
