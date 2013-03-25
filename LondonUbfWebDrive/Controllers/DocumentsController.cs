using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Repositories;

namespace LondonUbfWebDrive.Controllers
{
    public class DocumentsController : ApiController
    {
        // GET api/document
        public IEnumerable<Document> Get()
        {
            var repository = new DocumentRepository();
            var documents = repository.Read(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            return documents;
        }

        // GET api/document/5
        public IEnumerable<Document> Get(string id)
        {
            var repository = new DocumentRepository();
            var documents = repository.Read(id);

            return documents;
        }

        // POST api/document
        public void Post([FromBody]string value)
        {
        }

        // PUT api/document/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/document/5
        public void Delete(int id)
        {
        }
    }
}
