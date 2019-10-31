﻿using AutoMapper;
using RestaurantManager.BussinessLayer.DataTransferObjects;
using RestaurantManager.BussinessLayer.QueryObjects;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BussinessLayer.QueryObjects.Common;

namespace RestaurantManager.BussinessLayer.Services.Common
{
    public abstract class CrudQueryServiceBase<TEntity, TDto, TFilterDto> : ServiceBase
       where TFilterDto : FilterDtoBase, new()
       where TEntity : class, IEntity, new()
       where TDto : DtoBase
    {
        protected readonly IRepository<TEntity> Repository;

        protected readonly QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> Query;

        protected CrudQueryServiceBase(IMapper mapper, IRepository<TEntity> repository, QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> query) : base(mapper)
        {
            this.Query = query;
            this.Repository = repository;
        }

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        public virtual async Task<TDto> GetAsync(int entityId, bool withIncludes = true)
        {
            TEntity entity;
            if (withIncludes)
            {
                entity = await GetWithIncludesAsync(entityId);
            }
            else
            {
                entity = await Repository.GetAsync(entityId);
            }
            return entity != null ? Mapper.Map<TDto>(entity) : null;
        }

        /// <summary>
        /// Gets entity (with complex types) according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <returns>The DTO representing the entity</returns>
        protected abstract Task<TEntity> GetWithIncludesAsync(int entityId);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        public virtual int Create(TDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);
            Repository.Create(entity);
            return entity.Id;
        }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        public virtual async Task Update(TDto entityDto)
        {
            var entity = await GetWithIncludesAsync(entityDto.Id);
            Mapper.Map(entityDto, entity);
            Repository.Update(entity);
        }

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        public virtual void Delete(int entityId)
        {
            Repository.Delete(entityId);
        }

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        public virtual async Task<QueryResultDto<TDto, TFilterDto>> ListAllAsync()
        {
            return await Query.ExecuteQuery(new TFilterDto());
        }
    }
}
