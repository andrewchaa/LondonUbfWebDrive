using System.IO;
using System.Web;

namespace WebDrive.Domain.Model
{
    public class Thumbnail
    {
        public string Fullname { get; private set; }
        public string Name { get; private set; }
        public byte[] Content { get; private set; }
        public string ContentType { get; private set; }
        public bool IsDirectory { get; private set; }
        public string RelativePath { get; private set; }

        public Thumbnail(string fullName, string name, string relativePath, byte[] content, string contentType, bool isDirectory)
        {
            Fullname = fullName;
            Name = name;
            RelativePath = relativePath;
            Content = content;
            IsDirectory = isDirectory;
            ContentType = contentType;
        }

        public Thumbnail(string fullName, string name, string relativePath, string contentType, bool isDirectory)
        {
            Fullname = fullName;
            Name = name;
            RelativePath = relativePath;
            IsDirectory = isDirectory;
            ContentType = contentType;
            
        }
    }
}