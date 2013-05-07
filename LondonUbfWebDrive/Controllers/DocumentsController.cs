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
        private readonly IDocumentRepository _repository;
        private readonly string _baseFolder;

        public DocumentsController(IDocumentRepository repository)
        {
            _repository = repository;
            _baseFolder = ConfigurationManager.AppSettings["BaseFolder"];
        }

        // GET api/documents
        public IEnumerable<Document> Get()
        {
            var documents = _repository.List(_baseFolder);

            return documents;
        }

        // GET api/documents/text.txt
        public HttpResponseMessage Get(string path)
        {
            var documentBytes = _repository.Get(_baseFolder,  path);

            var stream = new MemoryStream(documentBytes);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);

            return response;
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
