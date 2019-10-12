using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Infrastucture.UnitOfWork
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
