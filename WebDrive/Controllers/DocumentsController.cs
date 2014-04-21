using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using log4net;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Controllers
{
    public class DocumentsController : ApiController
    {
        private readonly IReadDocumentService _service;
        private static readonly ILog Log = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected string BaseDirectory;

        public DocumentsController(IReadDocumentService service)
        {
            _service = service;
        }

        public IEnumerable<Document> Get()
        {
            var documents = _service.List(BaseDirectory, string.Empty);

            return documents;
        }

        public HttpResponseMessage Get(string path)
        {
            string fullname = Path.Combine(BaseDirectory, path);
            var document = _service.Get(fullname);
            var response = GetDownloadResponseFrom(document);

            return response;
        }

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
                    string to = Path.Combine(BaseDirectory + provider.FormData["selectedDir"], filename); 

                    Log.InfoFormat("A file({0}) is uploaded to temp folder", filename);
                    
                    var upload = new Upload(file.LocalFileName, to);
                    upload.Move();

                    Log.InfoFormat("A file ({0}) is moved to the target folder ({1})", filename, to);

                    filenames.Add(filename);
                }

                var jsonSerialiser = new JavaScriptSerializer();

                return new HttpResponseMessage { Content = new StringContent(jsonSerialiser.Serialize(filenames)) };
            }
            catch (System.Exception e)
            {
                Log.Error(e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.InnerException);
            }
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