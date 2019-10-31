using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BussinessLayer.DataTransferObjects;
using RestaurantManager.BussinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BussinessLayer.QueryObjects.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BussinessLayer.QueryObjects
{
    public class PaymentQueryObject : QueryObjectBase<PaymentDto, Payment, PaymentFilterDto, IQuery<Payment>>
    {
        public PaymentQueryObject(IMapper mapper, IQuery<Payment> query) : base(mapper, query)
        {
        }

        protected override IQuery<Payment> ApplyWhereClause(IQuery<Payment> query, PaymentFilterDto filter)
        {
            if (filter.PaymentId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Payment.Id), ValueComparingOperator.Equal,
                filter.PaymentId));
        }
    }
}
