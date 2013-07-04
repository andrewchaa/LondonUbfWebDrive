using System.IO;

namespace LondonUbfWebDrive.Domain.Model
{
    public class Document
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public bool IsFolder { get; private set; }
        public string DateModified { get; private set; }
        public string ImagePath { get; private set; }
        public byte[] Content { get; private set; }
        public string FileType
        {
            get 
            {
                return IsFolder ? "icon-folder-close orange" : "icon-file blue";
            }
        }

        public Document(string name, string fullName, string dateModified, bool isFolder, string imagePath)
        {
            Name = name;
            FullName = fullName;
            DateModified = dateModified;
            IsFolder = isFolder;
            ImagePath = imagePath;
        }

        public Document(string fullName, byte[] content)
        {
            FullName = fullName;
            Content = content;
        }

        public MemoryStream ToMemoryStream()
        {
            return new MemoryStream(Content);
        }
    }

}