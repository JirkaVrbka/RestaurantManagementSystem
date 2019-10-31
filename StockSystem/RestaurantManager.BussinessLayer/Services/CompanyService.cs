using AutoMapper;
using RestaurantManager.BussinessLayer.DataTransferObjects;
using RestaurantManager.BussinessLayer.QueryObjects;
using RestaurantManager.BussinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EntityFramework;
using RestaurantManager.Infrastructure.EntityFramework.UnitOfWork;
using RestaurantManager.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BussinessLayer.QueryObjects.Common;

namespace RestaurantManager.BussinessLayer.Services
{
    class CompanyService<TEntity, TDto, TFilterDto> : CrudQueryServiceBase<TEntity, TDto, TFilterDto>
        where TEntity : Company, new()
        // TODO use full companyDto (for create)
        where TDto : CompanyIdDto, new()
        where TFilterDto : FilterDtoBase, new()
    {
        protected CompanyService(IMapper mapper, IRepository<TEntity> repository, QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<TEntity> GetWithIncludesAsync(int entityId)
        {

            return await Repository.GetAsync(entityId, new string[] { "Persons", "Roles", "Items" });
        }

        protected  async Task<CompanyUsersDto> GetWithIncludesPersonAsync(int entityId)
        {

            var entity = await Repository.GetAsync(entityId, new string[] { "Persons" });
            return Mapper.Map<CompanyUsersDto>(entity);
        }
    }
}
