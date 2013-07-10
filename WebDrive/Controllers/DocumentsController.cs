using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using LondonUbfWebDrive.Domain.Model;
using LondonUbfWebDrive.Domain.Services;

namespace LondonUbfWebDrive.Controllers
{
    public class DocumentsController : ApiController
    {
        private readonly IReadDocumentService _service;
        private readonly string _baseFolder;

        public DocumentsController(IReadDocumentService service)
        {
            _service = service;
            _baseFolder = ConfigurationManager.AppSettings["BaseFolder"];
        }

        // GET api/documents
        public IEnumerable<Document> Get()
        {
            var documents = _service.List(_baseFolder);

            return documents;
        }

        // GET api/documents/text.txt
        public HttpResponseMessage Get(string path)
        {
            string fullname = Path.Combine(_baseFolder, path);
            var document = _service.Get(fullname);
            var response = GetDownloadResponseFrom(document);

            return response;
        }

        // POST api/document
        public async Task<HttpResponseMessage> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                StringBuilder sb = new StringBuilder(); // Holds the response body

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        sb.Append(string.Format("{0}: {1}\n", key, val));
                    }
                }

                // This illustrates how to get the file names for uploaded files.
                foreach (MultipartFileData file in provider.FileData)
                {
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);
                    sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
                }
                return new HttpResponseMessage()
                {
                    Content = new StringContent(sb.ToString())
                };
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
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
