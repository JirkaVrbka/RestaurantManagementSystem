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
    public class CompanyQueryObject : QueryObjectBase<CompanyDto, Company, CompanyFilterDto, IQuery<Company>>
    {
        public CompanyQueryObject(IMapper mapper, IQuery<Company> query) : base(mapper, query)
        {
        }

        protected override IQuery<Company> ApplyWhereClause(IQuery<Company> query, CompanyFilterDto filter)
        {
            if (string.IsNullOrEmpty(filter.Name))
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Company.Name), ValueComparingOperator.Equal, filter.Name));
        }
    }
}
