using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LondonUbfWebDrive.Domain
{
    public class DocumentRepository : IDocumentRepository
    {
        public IEnumerable<Document> Read(string path)
        {
            var directory = new DirectoryInfo(path);
            
            var documents = directory.GetDirectories().Select(d => new Document(d.Name, d.FullName, true)).ToList();
            documents.AddRange(directory.GetFiles().Select(f => new Document(f.Name, f.FullName, false)));

            return documents;
        }
    }
}