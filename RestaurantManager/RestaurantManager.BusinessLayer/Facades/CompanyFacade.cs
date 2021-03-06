﻿using System;
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
        private PaymentService _paymentService;
        private OrderItemService _orderItemService;
        private MenuItemService _menuItemService;
        public CompanyFacade(IUnitOfWorkProvider unitOfWorkProvider, 
            CompanyService companyService, 
            EmployeeService employeeService, 
            OrderService orderService, 
            PaymentService paymentService,
            OrderItemService orderItemService,
            MenuItemService menuItemService) : base(unitOfWorkProvider)
        {
            _employeeService = employeeService;
            this._companyService = companyService;
            this._employeeService = employeeService;
            _orderService = orderService;
            _paymentService = paymentService;
            _orderItemService = orderItemService;
            _menuItemService = menuItemService;
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

                    int companyId = (await _companyService.GetByIco(customer.Ico)).Id;
                    var payment = new PaymentDto
                    {
                        Amount = 300,
                        CompanyId = companyId,
                        DueDate = DateTime.Now.AddDays(30),
                        VariableNumber = "1212100" + companyId
                    };

                    _paymentService.Create(payment);
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

        public async Task<List<PaymentDto>> GetAllPayments(String employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithPayments(companyId)).Payments;
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

        public async Task<List<OrderWithFullDependencyDto>> GetAllOrdersWithOrderItems(String employeeEmail)
        {
            List<OrderWithFullDependencyDto> orders = new List<OrderWithFullDependencyDto>();
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                orders = await _orderService.GetOrderOfCompanyWithOrderItems(companyId);

            }

            return orders;
        }
        public async Task<List<OrderWithFullDependencyDto>> GetAllOrdersWithDependencies(String employeeEmail)
        {

            List<OrderWithFullDependencyDto> result = new List<OrderWithFullDependencyDto>();
            List<OrderDto> orders = new List<OrderDto>();
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                orders = await _orderService.GetOrderOfCompany(companyId);
                
            }

            Dictionary<int, MenuItemDto> menuItems = new Dictionary<int, MenuItemDto>();
            foreach (var o in orders)
            {
                using (UnitOfWorkProvider.Create())
                {
                    OrderWithFullDependencyDto order = new OrderWithFullDependencyDto
                    {
                        Id = o.Id,
                        OrderStartTime = o.OrderStartTime,
                        CompanyId = o.CompanyId,
                        OrderTable = o.OrderTable,
                        IsClosed = o.IsClosed,
                        Items = new List<OrderItemWithMenuItemDto>()
                    };
                    var items = await _orderItemService.GetWithMenuItemByOrderId(order.Id);
                    order.Items.AddRange(items);
                    result.Add(order);
                }
            }

            result.ForEach(o => o.Items.ForEach(i =>
            {
                if (!menuItems.ContainsKey(i.MenuItemId))
                {
                    menuItems.Add(i.MenuItemId, null);
                }
            }));

            foreach (var menuItemId in menuItems.Keys.ToList())
            {
                using (UnitOfWorkProvider.Create())
                {
                    menuItems[menuItemId] = await _menuItemService.GetAsync(menuItemId, false);
                }
            }

            result.ForEach(o => o.Items.ForEach(i => i.MenuItem = menuItems[i.MenuItemId]));

            return result;
        }

        public async Task<List<MenuItemDto>> GetAllMenuItems(String employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithMenuItems(companyId)).MenuItems;
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
                await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
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

        public async Task<List<OrderDto>> CloseTheDay(string employeeEmail, DateTime today)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithOrders(companyId)).Orders.FindAll(o => o.OrderStartTime.Year == today.Year && o.OrderStartTime.Month == today.Month && o.OrderStartTime.Day == today.Day);
            }
        }

        public async Task<List<OrderDto>> CloseTheDayBetween(string employeeEmail, DateTime startDate, DateTime endTime)
        {
            using (UnitOfWorkProvider.Create())
            {
                int companyId = (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
                return companyId == 0 ? null : (await _companyService.GetAsyncWithOrders(companyId)).Orders.FindAll(o => o.OrderStartTime >= startDate && o.OrderStartTime <= endTime);
            }
        }

        public async Task ClosePaidOrders(string employeeEmail)
        {
            List<OrderWithFullDependencyDto> unclosedOrders = new List<OrderWithFullDependencyDto>();
            int companyId = await GetCompanyId(employeeEmail);

            using (UnitOfWorkProvider.Create())
            {
                unclosedOrders = await _orderService.GetUnclosedOrdersOfCompany(companyId);
            }

            foreach(OrderWithFullDependencyDto o in unclosedOrders)
            {
                if (o.Items.TrueForAll(i => i.IsPaid))
                {
                    o.IsClosed = true;
                    using (UnitOfWorkProvider.Create())
                    {
                        await _orderService.Update(new OrderDto
                        {
                            CompanyId = o.CompanyId,
                            Id = o.Id,
                            IsClosed = true,
                            OrderStartTime = o.OrderStartTime,
                            OrderTable = o.OrderTable
                        });

                        await UnitOfWorkProvider.GetUnitOfWorkInstance().Commit();
                    }
                }
            }
        }

        private async Task<int> GetCompanyId(string employeeEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await _employeeService.GetEmployeeByEmail(employeeEmail)).CompanyId;
            }
        }
    }
}
