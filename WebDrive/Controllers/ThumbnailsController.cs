using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Controllers
{
    public class ThumbnailsController : ApiController
    {
        private readonly IFileDirectoryService _fileDirectoryService;

        public ThumbnailsController(IConfig config, IFileDirectoryService fileDirectoryService)
        {
            _fileDirectoryService = fileDirectoryService;
        }

        // GET api/thumbnails/path
        public WebEntity Get(string path)
        {
            return _fileDirectoryService.GetFile(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(path)));
        }

    }
}
