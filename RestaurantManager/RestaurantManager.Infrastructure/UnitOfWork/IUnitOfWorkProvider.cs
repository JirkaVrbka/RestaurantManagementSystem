using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider : IDisposable
    {
        /// <summary>
        /// Creates a new unit of work.
        /// </summary>
        IUnitOfWork Create();

        /// <summary>
        /// Gets the unit of work in the current scope.
        /// </summary>
        IUnitOfWork GetUnitOfWorkInstance();
    }
}
