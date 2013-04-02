namespace LondonUbfWebDrive.Domain
{
    public class Document
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public bool IsFolder { get; private set; }
        public string DateModified { get; private set; }

        public Document(string name, string fullName, string dateModified, bool isDirectory)
        {
            Name = name;
            FullName = fullName;
            DateModified = dateModified;
            IsFolder = isDirectory;
        }
    }
}