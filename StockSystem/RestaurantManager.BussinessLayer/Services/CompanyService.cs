using AutoMapper;
using RestaurantManager.BussinessLayer.DataTransferObjects;
using RestaurantManager.BussinessLayer.QueryObjects;
using RestaurantManager.BussinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.BussinessLayer.Services
{
    class CompanyService<TEntity, TDto, TFilterDto> : CrudQueryServiceBase<TEntity, TDto, TFilterDto>
        where TEntity : Company, new()
        where TDto : CompanyIdDto, new()
        where TFilterDto : FilterDtoBase, new()
    {
        protected CompanyService(IMapper mapper, IRepository<TEntity> repository, QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> query) : base(mapper, repository, query)
        {
        }

        protected override Task<TEntity> GetWithIncludesAsync(int entityId)
        {
           
        }
    }
}
