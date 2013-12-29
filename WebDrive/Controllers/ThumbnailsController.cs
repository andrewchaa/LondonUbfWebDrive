using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using LondonUbfWebDrive.Domain.Model;

namespace WebDrive.Controllers
{
    public class ThumbnailsController : ApiController
    {
//        // api/thumbnails/path
//        public FileContentResult Get(string path)
//        {
//            var image = new WebImage(@"C:\Users\andrew\Documents\Projects\WebDrive\WebDrive\Images\Desert.jpg")
//                .Resize(100, 100, true, true)
//                .GetBytes();
//
//            var result = new FileContentResult(image, "image/jpeg");
//            return result;
//        }

        // GET api/thumbnails/path
        public IEnumerable<Thumbnail> Get(string path)
        {
            path = @"C:\Users\andrew\Pictures\";
            var directory = new DirectoryInfo(path);
            var imageFileExtensions = new[] {".jpg", ".png", ".gif", ".tif"};
            
            var files = directory.EnumerateFiles().Where(f => imageFileExtensions.Contains(f.Extension));

            var thumbnails = new List<Thumbnail>();
            foreach (var file in files)
            {
                var thumbnail = new Thumbnail
                    {
                        Fullname = file.FullName,
                        Content = new WebImage(file.FullName).Resize(100, 100, true, true).GetBytes(),
                        ContentType = MimeMapping.GetMimeMapping(file.Name)
                    };
                thumbnails.Add(thumbnail);
            }

            return thumbnails;

        }
    }
}
