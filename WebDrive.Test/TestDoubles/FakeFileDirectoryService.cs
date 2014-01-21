using System.Collections.Generic;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace LondonUbfWebDrive.Test.TestDoubles
{
    internal class FakeFileDirectoryService : IFileDirectoryService
    {
        public IEnumerable<WebEntity> Directories { get; set; }
        public IEnumerable<WebEntity> Files { get; set; }
        public WebEntity File { get; set; }
        public byte[] Thumbnailimage { get; set; }
 
        public IEnumerable<WebEntity> EnumerateDirectories(string path)
        {
            return Directories;
        }

        public IEnumerable<WebEntity> EnumerateFiles(string path)
        {
            return Files;
        }

        public WebEntity GetFile(string fullName)
        {
            return File;
        }

        public byte[] GetThumbnailImage(string fullName)
        {
            return Thumbnailimage;
        }
    }
}