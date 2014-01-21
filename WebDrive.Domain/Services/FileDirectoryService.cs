using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Services
{
    public class FileDirectoryService : IFileDirectoryService
    {
        public IEnumerable<WebEntity> EnumerateDirectories(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.EnumerateDirectories().Select(d => WebEntity.Directory(d.Name, d.FullName));
        }

        public IEnumerable<WebEntity> EnumerateFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.EnumerateFiles().Select(f => WebEntity.File(f.Name, f.FullName, f.Extension));
        }

        public WebEntity GetFile(string fullName)
        {
            var file = new FileInfo(fullName);
            return WebEntity.File(file.Name, file.FullName, file.Extension);        }

        public byte[] GetThumbnailImage(string fullName)
        {
            return new WebImage(fullName).Resize(100, 100, true, true).GetBytes();
        }
    }

}
