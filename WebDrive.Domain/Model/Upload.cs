using System.IO;

namespace LondonUbfWebDrive.Domain.Model
{
    public class Upload
    {
        private readonly string _from;
        private readonly string _to;

        public Upload(string from, string to)
        {
            _from = from;
            _to = to;
        }


        public void Move()
        {
            if (File.Exists(_to)) File.Delete(_to);

            File.Move(_from, _to);
        }
    }
}