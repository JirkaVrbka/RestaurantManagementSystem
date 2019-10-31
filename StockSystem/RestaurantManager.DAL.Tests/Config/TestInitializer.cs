using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockSystem;

namespace RestaurantManager.DAL.Tests.Config
{
    class TestInitializer
    {
        public static DbContext InitializeDbContext()
        {
            var dbContext = new RestaurantManagerDbContext("RestaurantManagerTest");
            return dbContext;
        }
 
    }
}
