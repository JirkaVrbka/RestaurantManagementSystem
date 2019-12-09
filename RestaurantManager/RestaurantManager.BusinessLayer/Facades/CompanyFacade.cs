using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BusinessLayer.DTOs;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
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

        public async Task<int> RegisterCompany(CompanyDto companyCreateDto, string ownerEmail)
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

        public async Task Create(CompanyDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                _companyService.Create(employee);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(CompanyDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                _companyService.DeleteProduct(employee.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int employeeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _companyService.DeleteProduct(employeeId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(CompanyDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _companyService.Update(employee);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<CompanyDto> GetAsync(int employeeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _companyService.GetAsync(employeeId, false);
            }
        }
    }
}
