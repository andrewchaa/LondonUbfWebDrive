namespace LondonUbfWebDrive.Domain
{
    public class Breadcrumb
    {
        public string Name { get; private set; }
        public string Path { get; private set; }

        public Breadcrumb(string name, string path)
        {
            Name = name;
            Path = path;
        }

    }
}