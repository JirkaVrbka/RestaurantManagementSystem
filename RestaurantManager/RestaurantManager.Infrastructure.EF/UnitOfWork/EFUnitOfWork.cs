using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.Infrastructure.EF.UnitOfWork
{
    public class EFUnitOfWork : UnitOfWorkBase
    {
        public DbContext Context { get; }

        public EFUnitOfWork(Func<DbContext> dbContextFactory)
        {
            Context = dbContextFactory?.Invoke() ??
                      throw new ArgumentException("Unable to create DBContext [DbContextFactory is null]");
        }

        protected override async Task CommitCore()
        {
            await Context.SaveChangesAsync();
        }

        public override void Dispose()
        {
            Context.Dispose();
        }
    }
}
