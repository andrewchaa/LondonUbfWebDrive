using MongoDB.Bson;

namespace LondonUbfWebDrive.Domain
{
    public class DocumentMetadata
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public BsonDateTime DownloadTime { get; set; }
    }
}