using System.Configuration;
using LondonUbfWebDrive.Domain.Interfaces;
using OpenRasta.Web;

namespace WebDrive.App.Handlers
{
    public class FoldersHandler
    {
        private readonly IDocumentReader _documentReader;
        private string _baseFolder;

        public FoldersHandler(IDocumentReader documentReader)
        {
            _documentReader = documentReader;
            _baseFolder = ConfigurationManager.AppSettings["BaseFolder"];
        }

        public OperationResult Get(string folderName)
        {
            var documents = _documentReader.List(_baseFolder);

            return new OperationResult.OK() {ResponseResource = documents};
        }
    }
}