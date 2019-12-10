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
        private OrderService _orderService;
        public CompanyFacade(IUnitOfWorkProvider unitOfWorkProvider, CompanyService companyService, EmployeeService employeeService, OrderService orderService) : base(unitOfWorkProvider)
        {
            _employeeService = employeeService;
            this._companyService = companyService;
            this._employeeService = employeeService;
            _orderService = orderService;
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

        public async Task Update(CompanyUpdateNameDto company)
        {
            using (UnitOfWorkProvider.Create())
            {
                var dbCompany = await _companyService.GetAsync(company.Id);
                dbCompany.Name = company.Name;
                await _companyService.Update(dbCompany);
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


        public async Task<CompanyDto> FindCompanyByEmployeeEmail(String employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsync(companyId, false));
            }
        }

        public async Task<List<EmployeeDto>> GetAllEmployees(String employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithEmployees(companyId)).Employees;
            }
        }

        public async Task<List<OrderDto>> GetAllOrders(String employeeEmail, DateTime date)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithOrders(companyId)).Orders;
            }
        }

        public async Task<List<MenuItemDto>> GetAllMenuItems(String employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithMenuItems(companyId)).MenuItems;
            }
        }

        public async Task<List<StockItemDto>> GetAllStockItems(String employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithStock(companyId)).Stock;
            }
        }

        public async Task CreateNewOrderForCompany(string employeeEmail, OrderDto order)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                if (companyId == 0)
                {
                    throw new ArgumentException("Unable to create new order for company with employee having email " + employeeEmail +". No company found");
                }

                order.CompanyId = companyId;
                _orderService.Create(order);
            }
        }

        public async Task RegisterEmployee(EmployeeDto employee, string employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                if (companyId == 0)
                {
                    throw new ArgumentException("Unable to create new order for company with employee having email " + employeeEmail + ". No company found");
                }

                employee.CompanyId = companyId;
                await _employeeService.RegisterEmployeeAsync(employee);
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
            }
        }
    }
}
