using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using StockSystem;

namespace DAL
{
    class RestaurantManagerInitializer : DropCreateDatabaseAlways<RestaurantManagerDbContext>
    {
        protected override void Seed(RestaurantManagerDbContext context)
        {
            base.Seed(context);
        }
    }
}
