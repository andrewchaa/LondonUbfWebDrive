using System.Collections.Generic;
using System.IO;
using System.Linq;
using LondonUbfWebDrive.Domain.Model;

namespace LondonUbfWebDrive.Domain.Services
{
    public class ReadDocumentService : IReadDocumentService
    {
        public IEnumerable<Document> List(string baseFolder, string path)
        {
            var directory = new DirectoryInfo(Path.Combine(baseFolder, path));

            var documents = new List<Document>();
            documents.AddRange(GetFolders(baseFolder, directory));
            documents.AddRange(GetFiles(baseFolder, directory));

            return documents;
        }

        public IEnumerable<Document> List(string baseFolder)
        {
            return List(baseFolder, string.Empty);
        }

        public Document Get(string fullname)
        {
            var content = File.ReadAllBytes(fullname);

            return new Document(fullname, content);
        }

        private IEnumerable<Document> GetFiles(string baseFolder, DirectoryInfo directory)
        {
            return directory
                .GetFiles()
                .Where(f => !f.Name.StartsWith("~"))
                .Where(f => f.Extension != ".ini" && f.Extension != ".lnk" && f.Extension != ".config" &&
                    f.Extension != ".db" && f.Extension != ".exe")
                .Select(file => new Document(
                                file.FullName.Replace(baseFolder, string.Empty),
                                file.LastWriteTimeUtc.ToShortDateString(),
                                false,
                                GetFileImage(file.Extension))
                                ).ToList();
        }

        private string GetFileImage(string extension)
        {
            string image = "document.png";
            switch (extension)
            {
                case ".pdf": 
                    image = "pdf.png";
                    break;
                case ".zip":
                    image = "zip.png";
                    break;
                case ".doc":
                case ".docx":
                    image = "word.png";
                    break;
                case ".mp3":
                    image = "mp3.png";
                    break;
                case ".ppt":
                case ".pptx":
                    image = "powerpoint.png";
                    break;
                case ".xlsx":
                case ".xls":
                    image = "excel.png";
                    break;
            }
            
            return "/Images/" + image;
        }

        private IEnumerable<Document> GetFolders(string baseFolder, DirectoryInfo directory)
        {
            return directory.GetDirectories()
                            .Select(d => new Document(
                                             d.FullName.Replace(baseFolder, string.Empty),
                                             d.LastWriteTime.ToString(),
                                             true,
                                             "/Images/folder.png"
                                             ))
                            .ToList();
        }
    }


}