using System.IO;

namespace WebDrive.Domain.Model
{
    public class Document
    {
        public string FullName { get; private set; }
        public bool IsFolder { get; private set; }
        public string DateModified { get; private set; }
        public string ImagePath { get; private set; }
        public byte[] Content { get; private set; }
        public string Name
        {
            get { return Path.GetFileName(FullName); }
        }

        public string FileType
        {
            get 
            {
                return IsFolder ? "icon-folder-close orange" : "icon-file blue";
            }
        }

        public Document(string fullName, string dateModified, bool isFolder, string imagePath)
        {
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