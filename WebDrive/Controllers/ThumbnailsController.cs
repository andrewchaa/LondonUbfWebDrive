using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        // GET api/thumbnails/path
        public IEnumerable<Thumbnail> Get()
        {
            return _thumbnailsReader.List(_config.PictureDirectory);
        }

        // GET api/thumbnails/path
//        public IEnumerable<Thumbnail> Get(string path)
//        {
//            path = @"C:\Users\andrew\Pictures\";
//            var directory = new DirectoryInfo(path);
//            var imageFileExtensions = new[] {".jpg", ".png", ".gif", ".tif"};
//            
//            var files = directory.EnumerateFiles().Where(f => imageFileExtensions.Contains(f.Extension));
//
//            var thumbnails = files.Select(file => new Thumbnail(
//                file.FullName, 
//                new WebImage(file.FullName).Resize(100, 100, true, true).GetBytes(),
//                MimeMapping.GetMimeMapping(file.Name),
//                false)
//                );
//
//            return thumbnails.ToList();
//
//        }
    }

    public interface IReadThumbnails
    {
        IEnumerable<Thumbnail> List(string path);
    }

    public class ThumbnailsReader : IReadThumbnails
    {
        private readonly string[] _imageFileExtensions;

        public ThumbnailsReader()
        {
            _imageFileExtensions = new[] { ".jpg", ".png", ".gif", ".tif" };
        }

        public IEnumerable<Thumbnail> List(string path)
        {
            var directory = new DirectoryInfo(path);

            var thumbnails = GetThumbnails(directory);

            return thumbnails.ToList();
        }

        private IEnumerable<Thumbnail> GetThumbnails(DirectoryInfo directory)
        {
            var files = directory.EnumerateFiles().Where(f => _imageFileExtensions.Contains(f.Extension));

            var thumbnails = directory.EnumerateDirectories().Select(d => new Thumbnail(d.FullName, d.Name, null, string.Empty, true)).ToList();
            thumbnails.AddRange(files.Select(file => new Thumbnail(
                file.FullName,
                file.Name,
                new WebImage(file.FullName).Resize(100, 100, true, true).GetBytes(),
                MimeMapping.GetMimeMapping(file.Name),
                false)
                ));
            return thumbnails;
        }
    }
}
