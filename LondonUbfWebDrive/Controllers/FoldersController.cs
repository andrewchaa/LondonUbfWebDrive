using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LondonUbfWebDrive.Domain;

namespace LondonUbfWebDrive.Controllers
{
    public class FoldersController : ApiController
    {
        private readonly IDocumentRepository _repository;
        private readonly string _baseFolder;

        public FoldersController(IDocumentRepository repository)
        {
            _repository = repository;
            _baseFolder = ConfigurationManager.AppSettings["Directory"];
        }

        // GET api/folders
        public IEnumerable<Document> Get()
        {
            var documents = _repository.List(_baseFolder);

            return documents;
        }

        // GET api/folders/applications
        public IEnumerable<Document> Get(string id)
        {
            var documents = _repository.List(_baseFolder, id);

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
