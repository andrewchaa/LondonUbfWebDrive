using LondonUbfWebDrive.Domain;
using MongoDB.Driver;

namespace LondonUbfWebDrive.Infrastructure
{
    public class MongoDbHelper : IMongoDbHelper
    {
        private readonly IWebDriveConfig _webDriveConfig;
        private MongoClient _client;
        private MongoServer _server;
        private MongoDatabase _database;

        public MongoDbHelper(IWebDriveConfig webDriveConfig)
        {
            _webDriveConfig = webDriveConfig;
            _client = new MongoClient(_webDriveConfig.ConnectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase("webdrive");
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof (T).Name);
        }
    }
}