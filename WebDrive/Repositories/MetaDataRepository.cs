using System.Collections.Generic;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Infrastructure;
using MongoDB.Driver.Linq;

namespace LondonUbfWebDrive.Repositories
{
    public class MetaDataRepository : IMetaDataRepository
    {
        private readonly IMongoDbHelper _mongoDbHelper;

        public MetaDataRepository(IMongoDbHelper mongoDbHelper)
        {
            _mongoDbHelper = mongoDbHelper;
        }

        public void Save(DocumentMetadata documentMetadata)
        {
            var collection = _mongoDbHelper.GetCollection<DocumentMetadata>();
            collection.Insert(documentMetadata);
        }

        public IEnumerable<DocumentMetadata> List()
        {
            var collection = _mongoDbHelper.GetCollection<DocumentMetadata>();
            return collection.AsQueryable();
        }
    }
}