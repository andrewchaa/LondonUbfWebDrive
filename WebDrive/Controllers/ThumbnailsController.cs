using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Controllers
{
    public class ThumbnailsController : ApiController
    {
        private readonly IConfig _config;
        private readonly IReadThumbnails _thumbnailsReader;

        public ThumbnailsController(IConfig config, IReadThumbnails thumbnailsReader)
        {
            _config = config;
            _thumbnailsReader = thumbnailsReader;
        }

        // GET api/thumbnails
        public IEnumerable<Thumbnail> Get()
        {
            return _thumbnailsReader.List();
        }

        // GET api/thumbnails/path
        public IEnumerable<Thumbnail> Get(string path)
        {
            return _thumbnailsReader.List(path);
        }

    }
}
