using System.Collections.Generic;
using System.Web.Http;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Controllers
{
    public class FoldersControllerBase : ApiController
    {
        private readonly IReadDocumentService _service;
        protected string BaseDirectory;
        
        public FoldersControllerBase(IReadDocumentService service)
        {
            _service = service;
        }

        // GET api/folders
        public IEnumerable<Document> Get()
        {
            var documents = _service.List(BaseDirectory, string.Empty);
            return documents;
        }


        // GET api/folders/applications
        public IEnumerable<Document> Get(string path)
        {
            var documents = _service.List(BaseDirectory, path);
            return documents;
        }
    }
}