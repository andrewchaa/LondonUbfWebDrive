using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;

namespace LondonUbfWebDrive.Controllers
{
    public class FoldersController : ApiController
    {
        private readonly IDocumentReader _reader;
        private readonly string _baseFolder;

        public FoldersController(IDocumentReader reader)
        {
            _reader = reader;
            _baseFolder = ConfigurationManager.AppSettings["BaseFolder"];
        }

        // GET api/folders
        public IEnumerable<Document> Get()
        {
            var documents = _reader.List(_baseFolder);

            return documents;
        }

        // GET api/folders/applications
        public IEnumerable<Document> Get(string path)
        {
            var documents = _reader.List(_baseFolder, path);

            return documents;
        }

        // POST api/directory
        public void Post([FromBody]string value)
        {
        }

        // PUT api/directory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/directory/5
        public void Delete(int id)
        {
        }
    }
}
