using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BusinessLayer.DTOs;
using RestaurantManager.BusinessLayer.Facades.Common;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.BusinessLayer.Facades
{
    public class CompanyFacade : FacadeBase
    {
        private CompanyService _companyService;
        public CompanyFacade(IUnitOfWorkProvider unitOfWorkProvider, CompanyService companyService) : base(unitOfWorkProvider)
        {
            this._companyService = companyService;
        }

        public async Task<int> RegisterCompany(CompanyDto companyCreateDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await _companyService.RegisterCompanyAsync(companyCreateDto);
                    await uow.Commit();
                    return id;
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }
    }
}
