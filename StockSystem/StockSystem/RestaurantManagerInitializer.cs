using System.Data.Entity;
using StockSystem;

namespace RestaurantManager.DAL
{
    class RestaurantManagerInitializer : DropCreateDatabaseAlways<RestaurantManagerDbContext>
    {
        protected override void Seed(RestaurantManagerDbContext context)
        {
            base.Seed(context);
        }
    }
}
