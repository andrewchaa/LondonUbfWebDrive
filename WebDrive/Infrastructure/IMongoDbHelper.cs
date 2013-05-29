using LondonUbfWebDrive.Domain;
using MongoDB.Driver;

namespace LondonUbfWebDrive.Infrastructure
{
    public interface IMongoDbHelper
    {
        MongoCollection<T> GetCollection<T>();
    }
}