using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace LondonUbfWebDrive.Repositories
{
    public class MetaDataRepository : IMetaDataRepository
    {
        private readonly IWebDriveConfig _webDriveConfig;

        public MetaDataRepository(IWebDriveConfig webDriveConfig)
        {
            _webDriveConfig = webDriveConfig;
        }

        public void Save(DocumentMetadata documentMetadata)
        {
            var client = new MongoClient(_webDriveConfig.ConnectionString);

            var server = client.GetServer();
            var database = server.GetDatabase("webdrive");

            var collection = database.GetCollection<DocumentMetadata>("DocumentMetadata");
            collection.Insert(documentMetadata);
        }
    }
}