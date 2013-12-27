using System.Collections.Generic;
using System.Linq;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Domain.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Linq;
using WebDrive.Infrastructure;

namespace WebDrive.Repositories
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

        public IEnumerable<DocumentMetadata> ListRecentDownloads()
        {
            var collection = _mongoDbHelper.GetCollection<DocumentMetadata>();
            return collection.AsQueryable()
                             .OrderByDescending(c => c.DownloadTime)
                             .Take(30).ToList()
                             .GroupBy(c => c.FullName)
                             .Select(c => c.First());
;
        }

        public IEnumerable<DocumentMetadata> ListPopular()
        {
            var collection = _mongoDbHelper.GetCollection<DocumentMetadata>();
            var match = new BsonDocument
                {
                    {
                        "$group", new BsonDocument
                            {
                                {"_id", new BsonDocument
                                    {
                                        { "FullName", "$FullName" },
                                        { "Name", "$Name" }
                                    }
                                },
                                {
                                    "Count", new BsonDocument
                                        {
                                            {
                                                "$sum", 1
                                            }
                                        }
                                }
                            }
                    }
                };

            var project = new BsonDocument
                {
                    {
                        "$project", new BsonDocument
                            {
                                {"_id", 0},
                                {"FullName", "$_id.FullName"},
                                {"Name", "$_id.Name"},
                                {"Count", 1}
                            }
                    }
                };

            var pipeline = new[] {match, project};
            var result = collection.Aggregate(pipeline);
            return result.ResultDocuments.Select(BsonSerializer.Deserialize<DocumentMetadata>).ToList();
        }
    }
}