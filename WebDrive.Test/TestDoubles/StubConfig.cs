using WebDrive.Domain.Contracts;

namespace LondonUbfWebDrive.Test.TestDoubles
{
    public class StubConfig : IConfig
    {
        public string FileDirectory { get; private set; }
        public string ConnectionString { get; private set; }
        public string PictureDirectory { get; private set; }

        public StubConfig(string pictureDirectory)
        {
            PictureDirectory = pictureDirectory;
        }
    }
}