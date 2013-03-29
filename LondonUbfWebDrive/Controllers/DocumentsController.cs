using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Repositories;

namespace LondonUbfWebDrive.Controllers
{
    public class DocumentsController : ApiController
    {
        private string _directory;

        public DocumentsController() 
        {
            _directory = ConfigurationManager.AppSettings["Directory"];
        }

        // GET api/documents
        public IEnumerable<Document> Get()
        {
            var repository = new DocumentRepository();
            var documents = repository.List(_directory);

            return documents;
        }

        // GET api/documents/text.txt
        public HttpResponseMessage Get(string id)
        {
            var repository = new DocumentRepository();
            var documentBytes = repository.Get(Path.Combine(_directory, id));

            var stream = new MemoryStream(documentBytes);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return result;
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
