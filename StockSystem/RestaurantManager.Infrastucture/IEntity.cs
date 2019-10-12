using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Infrastucture
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string TableName { get; set; }
    }
}
