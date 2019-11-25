using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
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
            if (filter.PaymentId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Payment.Id), ValueComparingOperator.Equal,
                filter.PaymentId));
        }
    }
}
