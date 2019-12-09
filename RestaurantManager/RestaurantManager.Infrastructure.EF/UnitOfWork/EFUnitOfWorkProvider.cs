using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.Infrastructure.EF.UnitOfWork
{
    public class EFUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly Func<DbContext> dbContextFactory;

        public EFUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public override IUnitOfWork Create()
        {
            UowLocalInstance.Value = new EFUnitOfWork(dbContextFactory);
            return UowLocalInstance.Value;
        }
    }
}
