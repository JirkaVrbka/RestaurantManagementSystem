using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.Services;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.BusinessLayer.Facades
{
    public class PersonFacade : FacadeBase
    {
        private readonly PersonService personService;
        private readonly CompanyService companyService;
        public PersonFacade(IUnitOfWorkProvider unitOfWorkProvider, PersonService personService, CompanyService companyService) : base(unitOfWorkProvider)
        {
            this.personService = personService;
        }

        public async Task<int> RegisterCustomer(PersonCreateDto personCreateDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await personService.RegisterUserAsync(personCreateDto);
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
                return await personService.AuthorizeUserAsync(email, password);
            }
        }

    }
}
