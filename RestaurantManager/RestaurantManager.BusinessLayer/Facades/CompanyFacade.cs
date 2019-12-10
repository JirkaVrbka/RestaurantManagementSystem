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
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.Facades
{
    public class CompanyFacade : FacadeBase
    {
        private CompanyService _companyService;
        private EmployeeService _employeeService;
        public CompanyFacade(IUnitOfWorkProvider unitOfWorkProvider, CompanyService companyService, EmployeeService employeeService) : base(unitOfWorkProvider)
        {
            _employeeService = employeeService;
            this._companyService = companyService;
            this._employeeService = employeeService;
        }

        public async Task RegisterCompany(CompanyDto companyCreateDto, string ownerEmail)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    await _companyService.RegisterCompanyAsync(companyCreateDto);
                    await uow.Commit();
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task RegisterCompanyWithOwner(NewCustomerDto customer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var company = new CompanyDto
                    {
                        Ico = customer.Ico,
                        JoinDate = DateTime.Now,
                        Name = customer.Name
                    };

                    var employee = new EmployeeCreateDto()
                    {
                        Email = customer.Email,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PasswordHash = customer.Password,
                        Company = company,
                        Role = Role.Owner
                    };

                    // no need to create company as it is created in employee service by default
                    await _employeeService.RegisterCustomerAsync(employee);
                    await uow.Commit();
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
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

        public async Task<CompanyDto> GetAsyncByIco(int ico)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _companyService.GetByIco(ico);
            }
        }

        public async Task<CompanyDto> FindByUserEmail(String email)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(email)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsync(companyId, false));
            }
        }
    }
}
