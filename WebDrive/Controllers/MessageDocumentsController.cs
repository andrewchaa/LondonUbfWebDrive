﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;
using WebDrive.Domain.Services;
using log4net;

namespace WebDrive.Controllers
{
    public class MessageDocumentsController : ApiController
    {
        private readonly IReadDocumentService _service;
        private readonly IConfig _config;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MessageDocumentsController(IReadDocumentService service, IConfig config)
        {
            _service = service;
            _config = config;
            
        }

        // GET api/documents
        public IEnumerable<Document> Get()
        {
            var documents = _service.List(_config.MessageDirectory);

            return documents;
        }

        // GET api/documents/text.txt
        public HttpResponseMessage Get(string path)
        {
            string fullname = Path.Combine(_config.MessageDirectory, path);
            var document = _service.Get(fullname);
            var response = GetDownloadResponseFrom(document);

            return response;
        }

        // POST api/document
        public async Task<HttpResponseMessage> Post()
        {
            string tempStorage = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(tempStorage);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var filenames = new List<string>();

                foreach (var file in provider.FileData)
                {
                    string filename = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                    string to = Path.Combine(_config.FileDirectory + provider.FormData["selectedDir"], filename); 

                    _log.InfoFormat("A file({0}) is uploaded to temp folder", filename);
                    
                    var upload = new Upload(file.LocalFileName, to);
                    upload.Move();

                    _log.InfoFormat("A file ({0}) is moved to the target folder ({1})", filename, to);

                    filenames.Add(filename);
                }

                var jsonSerialiser = new JavaScriptSerializer();

                return new HttpResponseMessage { Content = new StringContent(jsonSerialiser.Serialize(filenames)) };
            }
            catch (System.Exception e)
            {
                _log.Error(e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.InnerException);
            }
        }

        // PUT api/document/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/document/5
        public void Delete(int id)
        {
        }

        private static HttpResponseMessage GetDownloadResponseFrom(Document document)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(document.ToMemoryStream());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = document.Name;
            return response;
        }
    }
}
