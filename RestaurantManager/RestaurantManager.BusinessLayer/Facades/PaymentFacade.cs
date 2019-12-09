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
    public class PaymentFacade : FacadeBase
    {
        private PaymentService _paymentService;
        public PaymentFacade(IUnitOfWorkProvider unitOfWorkProvider, PaymentService paymentService) : base(unitOfWorkProvider)
        {
            _paymentService = paymentService;
        }

        public async Task Create(PaymentDto payment)
        {
            using (UnitOfWorkProvider.Create())
            {
                _paymentService.Create(payment);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(PaymentDto payment)
        {
            using (UnitOfWorkProvider.Create())
            {
                _paymentService.DeleteProduct(payment.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int paymentId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _paymentService.DeleteProduct(paymentId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(PaymentDto payment)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _paymentService.Update(payment);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<PaymentDto> GetAsync(int paymentId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _paymentService.GetAsync(paymentId, false);
            }
        }
    }
}
