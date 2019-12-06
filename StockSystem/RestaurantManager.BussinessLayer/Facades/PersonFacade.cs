using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.DAL.Enums;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.BusinessLayer.Facades
{
    public class PersonFacade : FacadeBase
    {
        private readonly PersonService _personService;
        private readonly CompanyService _companyService;
        public PersonFacade(IUnitOfWorkProvider unitOfWorkProvider, PersonService personService, CompanyService companyService) : base(unitOfWorkProvider)
        {
            this._personService = personService;
        }

        public async Task<int> RegisterCustomer(CustomerCreateDto customerCreateDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var person = new PersonCreateDto()
                {
                    Email = customerCreateDto.Email,
                    FirstName = customerCreateDto.FirstName,
                    LastName = customerCreateDto.LastName,
                    Password = customerCreateDto.Password,
                    Role = Role.Owner
                };

                var company = new CompanyCreateDto()
                {
                    Ico = customerCreateDto.Ico,
                    JoinDate = DateTime.Now,
                    Name = customerCreateDto.CompanyName
                };

                try
                {
                    var id = await _personService.RegisterUserAsync(person);

                    var companyId = await _companyService.RegisterCompanyAsync(company);

                    await uow.Commit();
                    return id;
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task<(bool success, PersonDto person)> Login(string email, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _personService.AuthorizeUserAsync(email, password);
            }
        }

    }
}
