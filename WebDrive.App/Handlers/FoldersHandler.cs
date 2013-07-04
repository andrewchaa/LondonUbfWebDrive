using System.Configuration;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Domain.Services;
using OpenRasta.Web;

namespace WebDrive.App.Handlers
{
    public class FoldersHandler
    {
        private readonly IReadDocumentService _readDocumentService;
        private string _baseFolder;

        public FoldersHandler(IReadDocumentService readDocumentService)
        {
            _readDocumentService = readDocumentService;
            _baseFolder = ConfigurationManager.AppSettings["BaseFolder"];
        }

        public OperationResult Get(string folderName)
        {
            var documents = _readDocumentService.List(_baseFolder);

            return new OperationResult.OK() {ResponseResource = documents};
        }
    }
}