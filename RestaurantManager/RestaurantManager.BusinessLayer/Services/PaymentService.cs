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
    public class PaymentService : CrudQueryServiceBase<Payment, PaymentDto, PaymentFilterDto>
    {
        public PaymentService(IMapper mapper, IRepository<Payment> repository, QueryObjectBase<PaymentDto, Payment, PaymentFilterDto, IQuery<Payment>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<Payment> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, new string[] { nameof(Payment.Company) });
        }
    }
}
