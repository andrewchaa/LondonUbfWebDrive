using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Services
{
    public class FileDirectoryService : IFileDirectoryService
    {
        public IEnumerable<WebEntity> EnumerateDirectories(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.EnumerateDirectories().Select(d => new WebEntity(d.Name, d.FullName));
        }

        public IEnumerable<WebEntity> EnumerateFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.EnumerateFiles().Select(f => new WebEntity(f.Name, f.FullName, f.Extension));
        }
    }

}
