using System.Configuration;
using LondonUbfWebDrive.Domain.Model;

namespace LondonUbfWebDrive.Service
{
    public class ConfigService : IConfigService
    {
        public string BaseFolder { get { return ConfigurationManager.AppSettings["BaseFolder"]; } }

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

    }
}