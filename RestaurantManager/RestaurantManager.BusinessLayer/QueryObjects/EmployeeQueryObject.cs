using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Internal;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.BusinessLayer.QueryObjects
{
    public class EmployeeQueryObject : QueryObjectBase<EmployeeDto, Employee, EmployeeFilterDto, IQuery<Employee>>
    {
        public EmployeeQueryObject(IMapper mapper, IQuery<Employee> query) : base(mapper, query)
        {
        }

        protected override IQuery<Employee> ApplyWhereClause(IQuery<Employee> query, EmployeeFilterDto filter)
        {
            if (filter.Email.IsNullOrEmpty())
            {
                if (filter.CompanyId == 0)
                {
                    return query;
                }
                else
                {
                    return query.Where(new SimplePredicate(nameof(Employee.CompanyId), ValueComparingOperator.Equal, filter.CompanyId));
                }
            }
            else
            {
                if (filter.CompanyId == 0)
                {
                    return query.Where(new SimplePredicate(nameof(Employee.Email), ValueComparingOperator.Equal, filter.Email));
                }
                else
                {
                    return query.Where(
                            new CompositePredicate(new List<IPredicate>()
                            {
                                new SimplePredicate(nameof(Employee.Email), ValueComparingOperator.Equal, filter.Email),
                                new SimplePredicate(nameof(Employee.CompanyId), ValueComparingOperator.Equal, filter.CompanyId)
                            }, LogicalOperator.AND)
                        );
                }
            }
        }
    }
}
