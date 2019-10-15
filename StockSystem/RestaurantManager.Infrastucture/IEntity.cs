using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Infrastructure
{
    public interface IEntity
    {
        int Id { get; set; }
        //string TableName { get; set; }
    }
}
