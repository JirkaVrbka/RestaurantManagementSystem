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
    public class orderItemItemFacade : FacadeBase
    {
        private OrderItemService _OrderItemService;
        public orderItemItemFacade(IUnitOfWorkProvider unitOfWorkProvider, OrderItemService OrderItemService) : base(unitOfWorkProvider)
        {
            _OrderItemService = OrderItemService;
        }

        public async Task Create(OrderItemDto orderItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                _OrderItemService.Create(orderItem);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(OrderItemDto orderItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                _OrderItemService.DeleteProduct(orderItem.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int orderItemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _OrderItemService.DeleteProduct(orderItemId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(OrderItemDto orderItem)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _OrderItemService.Update(orderItem);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<OrderItemDto> GetAsync(int orderItemId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _OrderItemService.GetAsync(orderItemId, false);
            }
        }
    }
}