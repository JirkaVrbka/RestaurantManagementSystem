using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades.Common;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.BusinessLayer.Facades
{
    public class EmployeeFacade : FacadeBase
    {
        private EmployeeService _employeeService;
        public EmployeeFacade(IUnitOfWorkProvider unitOfWorkProvider, EmployeeService employeeService) : base(unitOfWorkProvider)
        {
            _employeeService = employeeService;
        }

        public async Task Create(EmployeeDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                _employeeService.Create(employee);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(EmployeeDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                _employeeService.DeleteProduct(employee.Id);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Delete(int employeeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                _employeeService.DeleteProduct(employeeId);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task Update(EmployeeDto employee)
        {
            using (UnitOfWorkProvider.Create())
            {
                await _employeeService.Update(employee);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }

        public async Task<EmployeeDto> GetAsync(int employeeId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _employeeService.GetAsync(employeeId, false);
            }
        }

        public async Task RegisterCustomer(NewCustomerDto customer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    await _employeeService.RegisterCustomerAsync(customer);
                    await uow.Commit();
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task<(bool success, string role)> Login(string email, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _employeeService.AuthorizeUserAsync(email, password);
            }
        }

        public async Task<EmployeeDto> GetByEmailAsync(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _employeeService.GetEmployeeByEmail(email);
            }
        }

        public async Task RegisterEmployee(EmployeeDto employee)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    await _employeeService.RegisterEmployeeAsync(employee);
                    await uow.Commit();
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }
    }
}
