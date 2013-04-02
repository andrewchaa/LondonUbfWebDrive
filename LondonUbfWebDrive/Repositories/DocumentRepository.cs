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

            var documents = new List<Document>();
            documents.Add(GetParentFolder(baseFolder));
            documents.AddRange(GetFolders(baseFolder, directory));
            documents.AddRange(GetFiles(baseFolder, directory));

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

        private static Document GetParentFolder(string baseFolder)
        {
            return new Document("...", baseFolder, string.Empty, true);
        }

        private static IEnumerable<Document> GetFiles(string baseFolder, DirectoryInfo directory)
        {
            return directory.GetFiles().Select(f => new Document(
                                                        f.Name, 
                                                        f.FullName.Replace(baseFolder, string.Empty), 
                                                        f.LastWriteTime.ToString(),
                                                        false
                                                        ));
        }

        private static IEnumerable<Document> GetFolders(string baseFolder, DirectoryInfo directory)
        {
            return directory.GetDirectories()
                            .Select(d => new Document(
                                             d.Name, 
                                             d.FullName.Replace(baseFolder, string.Empty),
                                             d.LastWriteTime.ToString(),
                                             true
                                             ))
                            .ToList();
        }
    }


}