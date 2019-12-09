using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BusinessLayer.DTOs;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.BusinessLayer.Facades.Common
{/*
    public class FacadeCrudBase<TDto> : FacadeBase
        where TDto : DtoBase

    {
        private CrudQueryServiceBase<IEntity, TDto, FilterDtoBase> service;
        public FacadeCrudBase(IUnitOfWorkProvider unitOfWorkProvider, CrudQueryServiceBase<IEntity,TDto,FilterDtoBase> service) : base(unitOfWorkProvider)
        {
            this.service = service;
        }

        public void Create(TDto entity)
        {
            using (UnitOfWorkProvider.Create())
            {

            }
        }

        
    }
    */
   
}
