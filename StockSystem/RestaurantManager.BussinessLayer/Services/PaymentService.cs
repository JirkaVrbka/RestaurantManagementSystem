using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    public class PaymentService : CrudQueryServiceBase<Payment, PaymentDto, PaymentFilterDto>
    {
        public PaymentService(IMapper mapper, IRepository<Payment> repository, QueryObjectBase<PaymentDto, Payment, PaymentFilterDto, IQuery<Payment>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<Payment> GetWithIncludesAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}
