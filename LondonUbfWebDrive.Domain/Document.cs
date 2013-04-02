namespace LondonUbfWebDrive.Domain
{
    public class Document
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public bool IsFolder { get; private set; }

        public Document(string name, string fullName, bool isDirectory)
        {
            Name = name;
            FullName = fullName;
            IsFolder = isDirectory;
        }
    }
}