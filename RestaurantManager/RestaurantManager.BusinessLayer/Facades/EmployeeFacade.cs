using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
