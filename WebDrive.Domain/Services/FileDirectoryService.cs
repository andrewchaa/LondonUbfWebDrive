using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDrive.Domain.Services
{
    public class FileDirectoryService : IFileDirectoryService
    {
        public IEnumerable<DirectoryInfo> EnumerateDirectories(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.EnumerateDirectories();
        }
    }

    public interface IFileDirectoryService
    {
        IEnumerable<DirectoryInfo> EnumerateDirectories(string path);
    }
}
