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


        public async Task Create(MenuItemDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                _menuItemService.Create(employee);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(MenuItemDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                _menuItemService.DeleteProduct(employee.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int employeeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _menuItemService.DeleteProduct(employeeId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(MenuItemDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _menuItemService.Update(employee);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<MenuItemDto> GetAsync(int employeeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _menuItemService.GetAsync(employeeId, false);
            }
        }
    }

}
