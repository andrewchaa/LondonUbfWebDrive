namespace LondonUbfWebDrive.Domain
{
    public class Document
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public bool IsFolder { get; private set; }
        public string DateModified { get; private set; }
        public string ImagePath { get; private set; }
        public byte[] ContentBytes { get; private set; }

        public Document(string name, string fullName, string dateModified, bool isFolder, string imagePath)
        {
            Name = name;
            FullName = fullName;
            DateModified = dateModified;
            IsFolder = isFolder;
            ImagePath = imagePath;
        }
    }

}