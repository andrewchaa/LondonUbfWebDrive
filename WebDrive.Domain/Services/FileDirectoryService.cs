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
            return directory.EnumerateDirectories().Select(d => new WebEntity(d.Name, d.FullName, string.Empty));
        }

        public IEnumerable<WebEntity> EnumerateFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.EnumerateFiles().Select(f => new WebEntity(f.Name, f.FullName, f.Extension));
        }

        public WebEntity GetFile(string fullName)
        {
            var file = new FileInfo(fullName);
            var entity = new WebEntity(file.Name, file.FullName, file.Extension);

            if (entity.IsImage)
                return new WebEntity(entity.Name, entity.FullName, entity.Extension, GetThumbnailImage(fullName));

            return entity;
        }

        public byte[] GetThumbnailImage(string fullName)
        {
            return new WebImage(fullName).Resize(100, 100, true, true).GetBytes();
        }
    }

}
