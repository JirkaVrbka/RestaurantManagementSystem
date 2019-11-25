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
    public class PersonQueryObject : QueryObjectBase<PersonDto, Person, PersonFilterDto, IQuery<Person>>
    {
        public PersonQueryObject(IMapper mapper, IQuery<Person> query) : base(mapper, query)
        {
        }

        protected override IQuery<Person> ApplyWhereClause(IQuery<Person> query, PersonFilterDto filter)
        {
            if (string.IsNullOrEmpty(filter.Name))
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Person.FirstName), ValueComparingOperator.Equal,
                filter.Name));
        }
    }
}
