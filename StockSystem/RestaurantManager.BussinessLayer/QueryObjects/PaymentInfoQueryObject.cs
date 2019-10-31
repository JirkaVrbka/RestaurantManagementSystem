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
