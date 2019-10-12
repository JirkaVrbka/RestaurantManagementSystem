using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Infrastructure
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string TableName { get; set; }
    }
}
