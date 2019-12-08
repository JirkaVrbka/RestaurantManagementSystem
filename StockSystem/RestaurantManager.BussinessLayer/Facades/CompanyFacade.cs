using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
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

        public async Task<int> RegisterCompany(CompanyCreateDto companyCreateDto)
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

        public async Task<List<PersonDto>> GetEmployees(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var company = await _companyService.GetWithIncludesPeopleAsync(id);
                return company.People;
            }
        }
    }
}
