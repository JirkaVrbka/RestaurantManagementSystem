using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BusinessLayer.QueryObjects
{
    public class PaymentQueryObject : QueryObjectBase<PaymentDto, Payment, PaymentFilterDto, IQuery<Payment>>
    {
        public PaymentQueryObject(IMapper mapper, IQuery<Payment> query) : base(mapper, query)
        {
        }

        protected override IQuery<Payment> ApplyWhereClause(IQuery<Payment> query, PaymentFilterDto filter)
        {
            return filter.CompanyId == 0
                ? query
                : query.Where(new SimplePredicate(nameof(Payment.CompanyId), ValueComparingOperator.Equal,
                    filter.CompanyId));
        }
    }
}
