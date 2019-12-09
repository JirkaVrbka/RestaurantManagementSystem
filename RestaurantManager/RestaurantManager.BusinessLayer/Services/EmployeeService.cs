using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Utils.EntityEnums;

namespace RestaurantManager.BusinessLayer.Services
{
    public class EmployeeService : CrudQueryServiceBase<Employee, EmployeeDto, EmployeeFilterDto>
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        private readonly IRepository<Employee> _repository;

        private readonly QueryObjectBase<EmployeeDto, Employee, EmployeeFilterDto, IQuery<Employee>>
            _query;

        public EmployeeService(IMapper mapper, IRepository<Employee> repository, QueryObjectBase<EmployeeDto, Employee, EmployeeFilterDto, IQuery<Employee>> query) : base(mapper, repository, query)
        {
            _repository = repository;
            _query = query;
        }

        protected override Task<Employee> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, nameof(Employee.Company));
        }

        public async Task<List<EmployeeDto>> GetEmployeesOfCompany(int companyId)
        {
            var queryResult = await Query.ExecuteQuery(new EmployeeFilterDto { CompanyId = companyId });
            return queryResult.Items.ToList();
        }

        public async Task<EmployeeDto> GetEmployeeByEmail(String email)
        {
            var queryResult = await Query.ExecuteQuery(new EmployeeFilterDto { Email = email });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task RegisterCustomerAsync(NewCustomerDto userDto)
        {
            var customer = Mapper.Map<Employee>(userDto);

            if (await GetEmployeeByEmail(customer.Email) != null)
            {
                throw new ArgumentException();
            }

            var password = CreateHash(userDto.Password);
            customer.PasswordHash = password.Item1;
            customer.PasswordSalt = password.Item2;

            _repository.Create(customer);
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }

        public async Task<(bool success, string role)> AuthorizeUserAsync(string email, string password)
        {
            var userResult = await _query.ExecuteQuery(new EmployeeFilterDto() { Email = email });
            var user = userResult.Items.SingleOrDefault();

            var succ = user != null && VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password);
            var role = user?.Role.ToString() ?? "";
            return (succ, role);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        public async Task RegisterEmployeeAsync(EmployeeDto employee)
        {
            var customer = Mapper.Map<Employee>(employee);

            if (await GetEmployeeByEmail(customer.Email) != null)
            {
                throw new ArgumentException();
            }

            var password = CreateHash(employee.Password);
            customer.PasswordHash = password.Item1;
            customer.PasswordSalt = password.Item2;

            _repository.Create(customer);
        }
    }
}
