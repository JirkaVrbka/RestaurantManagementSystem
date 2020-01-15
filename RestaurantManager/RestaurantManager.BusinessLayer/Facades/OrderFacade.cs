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
    public class OrderFacade : FacadeBase
    {
        private OrderService _orderService;
        private OrderWithDependenciesService _orderWithDependenciesService;
        public OrderFacade(IUnitOfWorkProvider unitOfWorkProvider, OrderService orderService, OrderWithDependenciesService orderWithDependenciesService) : base(unitOfWorkProvider)
        {
            _orderService = orderService;
            _orderWithDependenciesService = orderWithDependenciesService;
        }

        public async Task<List<OrderDto>> GetStockItems(int companyId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _orderService.GetOrderOfCompany(companyId);
            }
        }

        public async Task Create(OrderDto order)
        {
            using (UnitOfWorkProvider.Create())
            {
                _orderService.Create(order);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(OrderDto order)
        {
            using (UnitOfWorkProvider.Create())
            {
                _orderService.DeleteProduct(order.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int orderId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _orderService.DeleteProduct(orderId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(OrderDto order)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _orderService.Update(order);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<OrderDto> GetAsync(int orderId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _orderService.GetAsync(orderId, false);
            }
        }

        public void CloseTheDay(string identityName, DateTime today)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderWithFullDependencyDto> GetAsyncWithDependencies(int orderId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _orderWithDependenciesService.GetAsyncWithOrderItems(orderId);
            }
        }
    }
}
