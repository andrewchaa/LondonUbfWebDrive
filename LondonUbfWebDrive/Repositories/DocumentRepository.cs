using System.Collections.Generic;
using System.IO;
using System.Linq;
using LondonUbfWebDrive.Domain;

namespace LondonUbfWebDrive.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public IEnumerable<Document> Read(string path)
        {
            var directory = new DirectoryInfo(path);
            
            var documents = directory.GetDirectories().Select(d => new Document(d.Name, d.FullName.Replace(path, string.Empty), true)).ToList();
            documents.AddRange(directory.GetFiles().Select(f => new Document(f.Name, f.FullName.Replace(path, string.Empty), false)));

            return documents;
        }
    }
}