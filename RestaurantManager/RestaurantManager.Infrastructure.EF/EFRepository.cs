using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.Infrastructure.EF.UnitOfWork;
using RestaurantManager.Infrastructure.UnitOfWork;

namespace RestaurantManager.Infrastructure.EF
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider provider;

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        protected DbContext Context => ((EFUnitOfWork)provider.GetUnitOfWorkInstance()).Context;

        public EFRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(int id, params string[] includes)
        {
            DbQuery<TEntity> ctx = Context.Set<TEntity>();
            foreach (var include in includes)
            {
                ctx = ctx.Include(include);
            }
            return await ctx
                .SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public void Update(TEntity entity)
        {
            var foundEntity = Context.Set<TEntity>().Find(entity.Id);
            Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }
    }
}
