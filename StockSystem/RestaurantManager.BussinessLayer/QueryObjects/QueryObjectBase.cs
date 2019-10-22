using AutoMapper;
using RestaurantManager.BussinessLayer.DataTransferObjects;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.QueryObjects
{
    public abstract class QueryObjectBase<TDto, TEntity, TFilter, TQuery>
        where TFilter : FilterDtoBase
        where TQuery : IQuery<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IMapper mapper;

        protected readonly IQuery<TEntity> Query;

        protected QueryObjectBase(IMapper mapper, TQuery query)
        {
            this.mapper = mapper;
            this.Query = query;
        }

        protected abstract IQuery<TEntity> ApplyWhereClause(IQuery<TEntity> query, TFilter filter);

        public virtual async Task<QueryResultDto<TDto, TFilter>> ExecuteQuery(TFilter filter)
        {
            var query = ApplyWhereClause(Query, filter);
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query = query.SortBy(filter.SortCriteria, filter.SortAscending);
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            var queryResult = await query.ExecuteAsync();

            var queryResultDto = mapper.Map<QueryResultDto<TDto, TFilter>>(queryResult);
            queryResultDto.Filter = filter;
            return queryResultDto;
        }
    }
}

