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
    public class StockItemFacade : FacadeBase
    {
        private StockItemService _stockItemService;
        public StockItemFacade(IUnitOfWorkProvider unitOfWorkProvider, StockItemService stockItemService) : base(unitOfWorkProvider)
        {
            _stockItemService = stockItemService;
        }

        public async Task<List<StockItemDto>> GetStockItems(int companyId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _stockItemService.GetStockItemsOfCompany(companyId);
            }
        }

        public async Task Create(StockItemDto stockItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                _stockItemService.Create(stockItem);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(StockItemDto stockItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                _stockItemService.DeleteProduct(stockItem.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int stockItemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _stockItemService.DeleteProduct(stockItemId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(StockItemDto stockItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _stockItemService.Update(stockItem);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<StockItemDto> GetAsync(int stockItemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _stockItemService.GetAsync(stockItemId, false);
            }
        }
    }
}
