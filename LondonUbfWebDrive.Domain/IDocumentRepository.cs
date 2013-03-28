using System.Collections.Generic;
using System.IO;

namespace LondonUbfWebDrive.Domain
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> List(string path);
        byte[] Get(string path);
    }
}