using LondonUbfWebDrive.Domain.Model;
using MongoDB.Driver;

namespace WebDrive.Infrastructure
{
    public class MongoDbHelper : IMongoDbHelper
    {
        private readonly IConfigService _configService;
        private readonly MongoClient _client;
        private readonly MongoServer _server;
        private readonly MongoDatabase _database;

        public MongoDbHelper(IConfigService configService)
        {
            _configService = configService;
            _client = new MongoClient(_configService.ConnectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase("webdrive");
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof (T).Name);
        }
    }
}