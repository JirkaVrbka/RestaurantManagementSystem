using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.BusinessLayer.QueryObjects.Common;
using RestaurantManager.BusinessLayer.Services.Common;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Services
{
    public class PersonService : ServiceBase
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        private readonly IRepository<Person> personRepository;
        private readonly QueryObjectBase<PersonDto, Person, PersonFilterDto, IQuery<Person>> personQueryObject;

        public PersonService(IMapper mapper, IRepository<Person> repository, QueryObjectBase<PersonDto, Person, PersonFilterDto, IQuery<Person>> query) : base(mapper)
        {
            this.personRepository = repository;
            this.personQueryObject = query;
        }

        public async Task<int> RegisterUserAsync(PersonCreateDto personCreateDto)
        {
            var person = Mapper.Map<Person>(personCreateDto);

            if (await GetIfPersonExistsAsync(person.Email))
            {
                throw new ArgumentException("Person with this email already exists!");
            }

            var password = CreateHash(personCreateDto.Password);
            person.HashedPassword = password;

            personRepository.Create(person);

            return person.Id;
        }

        public async Task<(bool success, PersonDto person)> AuthorizeUserAsync(string email, string password)
        {
            var personResult = await personQueryObject.ExecuteQuery(new PersonFilterDto() { Email = email });
            var person = personResult.Items.SingleOrDefault();

            var succ = person != null && VerifyHashedPassword(person.HashedPassword, password);
            return (succ, person);
        }

        private async Task<bool> GetIfPersonExistsAsync(string email)
        {
            var queryResult = await personQueryObject.ExecuteQuery(new PersonFilterDto() { Email = email });
            return (queryResult.Items.Count() == 1);
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            using (var deriveBytes = new Rfc2898DeriveBytes(password,0, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private string CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Convert.ToBase64String(subkey);
            }
        }
    }
}
