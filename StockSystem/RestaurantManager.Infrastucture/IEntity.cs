

namespace RestaurantManager.Infrastructure
{
    public interface IEntity
    {
        int Id { get; set; }
        string TableName { get; }
    }
}
