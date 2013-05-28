using System.Configuration;

namespace LondonUbfWebDrive.Infrastructure
{
    public class WebDriveConfiguration : IWebDriveConfig
    {
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