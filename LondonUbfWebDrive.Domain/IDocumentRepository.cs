using System.Collections.Generic;

namespace LondonUbfWebDrive.Domain
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> Read(string path);
    }
}