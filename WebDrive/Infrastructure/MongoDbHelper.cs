using MongoDB.Driver;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Services;

namespace WebDrive.Infrastructure
{
    public class MongoDbHelper : IMongoDbHelper
    {
        private readonly IConfig _config;
        private readonly MongoClient _client;
        private readonly MongoServer _server;
        private readonly MongoDatabase _database;

        public MongoDbHelper(IConfig config)
        {
            _config = config;
            _client = new MongoClient(_config.ConnectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase("webdrive");
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof (T).Name);
        }
    }
}