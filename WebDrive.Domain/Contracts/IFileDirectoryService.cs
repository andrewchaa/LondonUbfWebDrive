using System.Collections.Generic;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Contracts
{
    public interface IFileDirectoryService
    {
        IEnumerable<WebEntity> EnumerateDirectories(string path);
        IEnumerable<WebEntity> EnumerateFiles(string path);
    }
}