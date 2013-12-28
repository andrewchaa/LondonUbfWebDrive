using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using LondonUbfWebDrive.Domain.Model;
using LondonUbfWebDrive.Domain.Services;

namespace WebDrive.Controllers
{
    public class FoldersController : ApiController
    {
        private readonly IReadDocumentService _service;
        private readonly string _baseFolder;

        public FoldersController(IReadDocumentService service)
        {
            _service = service;
            _baseFolder = ConfigurationManager.AppSettings["BaseFolder"];
        }

        // GET api/folders
        public IEnumerable<Document> Get()
        {
            var documents = _service.List(_baseFolder);

            return documents;
        }

        // GET api/folders/applications
        public IEnumerable<Document> Get(string path)
        {
            var documents = _service.List(_baseFolder, path);

            return documents;
        }
    }
}
