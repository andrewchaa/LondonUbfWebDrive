using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LondonUbfWebDrive.Domain.Model;
using LondonUbfWebDrive.Domain.Services;
using log4net;

namespace LondonUbfWebDrive.Controllers
{
    public class DocumentsController : ApiController
    {
        private readonly IReadDocumentService _service;
        private readonly IConfigService _configService;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DocumentsController(IReadDocumentService service, IConfigService configService)
        {
            _service = service;
            _configService = configService;
            
        }

        // GET api/documents
        public IEnumerable<Document> Get()
        {
            var documents = _service.List(_configService.BaseFolder);

            return documents;
        }

        // GET api/documents/text.txt
        public HttpResponseMessage Get(string path)
        {
            string fullname = Path.Combine(_configService.BaseFolder, path);
            var document = _service.Get(fullname);
            var response = GetDownloadResponseFrom(document);

            return response;
        }

        // POST api/document
        public async Task<HttpResponseMessage> Post()
        {
            string tempStorage = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(tempStorage);
            string selectedDir = provider.FormData["selectedDir"];

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var filenames = new List<string>();

                foreach (var file in provider.FileData)
                {
                    string filename = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                    string to = Path.Combine(_configService.BaseFolder + selectedDir, filename); 
                    
                    var upload = new Upload(file.LocalFileName, to);
                    upload.Move();

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
