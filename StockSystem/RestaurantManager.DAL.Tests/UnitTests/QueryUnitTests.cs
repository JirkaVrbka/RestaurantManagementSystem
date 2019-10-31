using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EntityFramework;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.Query.Predicates;
using RestaurantManager.Infrastructure.Query.Predicates.Operators;

namespace RestaurantManager.DAL.Tests.UnitTests
{
    [TestClass]
    public class QueryUnitTests : UnitTestsBase
    {
        
        [TestMethod]
        public async Task ExecuteAsync_SimpleWherePredicate_ReturnsCorrectQueryResult()
        {
            var companies = createCompanies();

            var companyQuery = new EntityFrameworkQuery<Company>(Provider);
            var expectedQueryResult = new QueryResult<Company>(companies.FindAll(c => c.Name.Contains("Kolo")), companies.FindAll(c => c.Name.Contains("Kolo")).Count);

            var predicate = new SimplePredicate(nameof(Company.Name), ValueComparingOperator.StringContains, "Kolo");
            var actualQueryResult = await companyQuery.Where(predicate).ExecuteAsync();
            

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

        private List<Company> createCompanies()
        {
            List<Company> companies = new List<Company>
            {
                new Company
                {
                    Name = "Kolotoc",
                    Ico = 111,
                    JoinDate = DateTime.Now,
                    Location = "Prague"
                    
                },
                new Company
                {
                    Name = "Pekarna",
                    Ico = 111,
                    JoinDate = DateTime.Now,
                    Location = "Prague"

                },
                new Company
                {
                    Name = "Kolomaznik",
                    Ico = 111,
                    JoinDate = DateTime.Now,
                    Location = "Prague"

                },
                new Company
                {
                    Name = "Koliste",
                    Ico = 111,
                    JoinDate = DateTime.Now,
                    Location = "Prague"

                },
                new Company
                {
                    Name = "Lokomotiva",
                    Ico = 111,
                    JoinDate = DateTime.Now,
                    Location = "Prague"

                }
            };
            IRepository<Company> repository = new EntityFrameworkRepository<Company>(Provider);
            companies.ForEach(c => repository.Create(c));
            UnitOfWork.Commit();

            return companies;
        }
    }
}
