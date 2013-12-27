using MongoDB.Driver;

namespace WebDrive.Infrastructure
{
    public interface IMongoDbHelper
    {
        MongoCollection<T> GetCollection<T>();
    }
}