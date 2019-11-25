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
    public class PaymentInfoQueryObject : QueryObjectBase<PaymentInfoDto, PaymentInfo, PaymentInfoFilterDto, IQuery<PaymentInfo>>
    {
        public PaymentInfoQueryObject(IMapper mapper, IQuery<PaymentInfo> query) : base(mapper, query)
        {
        }

        protected override IQuery<PaymentInfo> ApplyWhereClause(IQuery<PaymentInfo> query, PaymentInfoFilterDto filter)
        {
            if (filter.PaymentInfoId == 0)
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(PaymentInfo.Id), ValueComparingOperator.Equal,
                filter.PaymentInfoId));
        }
    }
}
