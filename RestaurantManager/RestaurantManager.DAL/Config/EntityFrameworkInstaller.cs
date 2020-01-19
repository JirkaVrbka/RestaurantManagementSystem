using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RestaurantManager.Infrastructure;
using RestaurantManager.Infrastructure.EF;
using RestaurantManager.Infrastructure.EF.UnitOfWork;
using RestaurantManager.Infrastructure.Query;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.DAL.Config
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        //release db
        internal const string ConnectionString = "Server=tcp:restaurantmanager-server.database.windows.net,1433;Initial Catalog=restaurantmanager-db;Persist Security Info=False;User ID=dbadmin;Password=KruteHeslo1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        //for debug
        //internal const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=RestaurantManager11;Trusted_Connection=True;MultipleActiveResultSets=True";


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new RestaurantManagerDbContext())
                    .LifestyleTransient()
                    .IsFallback(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EFUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EFRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EFQuery<>))
                    .LifestyleTransient()
            );
        }
    }
}
