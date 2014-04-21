using System.Configuration;
using WebDrive.Domain.Contracts;

namespace WebDrive.Service
{
    public class Config : IConfig
    {
        public string FileDirectory { get { return ConfigurationManager.AppSettings["FileDirectory"]; } }
        public string MessageDirectory { get { return ConfigurationManager.AppSettings["MessageDirectory"]; } }
        public string QuestionSheetDirectory { get { return ConfigurationManager.AppSettings["QuestionSheetDirectory"]; } }

        public string ConnectionString
        {
            get
            {
                string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = "mongodb://localhost";
                }

                return connectionString;
            }
        }

        public string PictureDirectory
        {
            get { return ConfigurationManager.AppSettings["PictureDirectory"]; }
        }

    }
}