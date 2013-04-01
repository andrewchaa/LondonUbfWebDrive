using System.Collections.Generic;
using System.IO;
using System.Linq;
using LondonUbfWebDrive.Domain;

namespace LondonUbfWebDrive.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public IEnumerable<Document> List(string baseFolder, string path)
        {

            var directory = new DirectoryInfo(Path.Combine(baseFolder, path));
            
            var documents = directory.GetDirectories().Select(d => new Document(d.Name, d.FullName.Replace(baseFolder, string.Empty), true)).ToList();
            documents.AddRange(directory.GetFiles().Select(f => new Document(f.Name, f.FullName.Replace(baseFolder, string.Empty), false)));

            return documents;
        }

        public IEnumerable<Document> List(string baseFolder)
        {
            return List(baseFolder, string.Empty);
        }

        public byte[] Get(string baseFolder, string path)
        {
            return File.ReadAllBytes(Path.Combine(baseFolder, path));
        }
    }


}