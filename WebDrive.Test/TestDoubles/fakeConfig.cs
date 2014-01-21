using WebDrive.Domain.Contracts;

namespace LondonUbfWebDrive.Test.TestDoubles
{
    public class FakeConfig : IConfig
    {
        public string FileDirectory { get; private set; }
        public string ConnectionString { get; private set; }
        public string PictureDirectory { get; set; }

    }
}