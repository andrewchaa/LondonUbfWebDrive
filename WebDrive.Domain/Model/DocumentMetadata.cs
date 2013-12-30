using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebDrive.Domain.Model
{
    public class DocumentMetadata
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DownloadTime { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }
        public int Count { get; set; }

    }
}