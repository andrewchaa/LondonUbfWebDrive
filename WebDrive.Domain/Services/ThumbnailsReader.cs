using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Services
{
    public class ThumbnailsReader : IReadThumbnails
    {
        private readonly IConfig _config;
        private readonly string[] _imageFileExtensions;

        public ThumbnailsReader(IConfig config)
        {
            _config = config;
            _imageFileExtensions = new[] { ".jpg", ".png", ".gif", ".tif" };
        }

        public IEnumerable<Thumbnail> List()
        {
            var directory = new DirectoryInfo(_config.PictureDirectory);

            var thumbnails = GetDirectories(directory).ToList();
            thumbnails.AddRange(GetThumbnails(directory));

            return thumbnails.ToList();
        }

        public IEnumerable<Thumbnail> List(string path)
        {
            var directory = new DirectoryInfo(Path.Combine(_config.PictureDirectory, path));

            var thumbnails = GetDirectories(directory).ToList();
            thumbnails.AddRange(GetThumbnails(directory));

            return thumbnails.ToList();
        }

        private IEnumerable<Thumbnail> GetDirectories(DirectoryInfo directory)
        {
            return directory.EnumerateDirectories().Select(d =>
                new Thumbnail(d.FullName, d.Name, GetRelativePath(d.FullName), null, string.Empty, true)).ToList();
        }

        private string GetRelativePath(string fullname)
        {
            return fullname.Replace(_config.PictureDirectory, string.Empty);
        }

        private IEnumerable<Thumbnail> GetThumbnails(DirectoryInfo directory)
        {
            var files = directory.EnumerateFiles().Where(f => _imageFileExtensions.Contains(f.Extension));

            var thumbnails = files.Select(t => new Thumbnail(
                                                         t.FullName,
                                                         t.Name,
                                                         GetRelativePath(t.FullName),
                                                         new WebImage(t.FullName).Resize(100, 100, true, true).GetBytes(),
                                                         MimeMapping.GetMimeMapping(t.Name),
                                                         false)
                                    );
            return thumbnails;
        }
    }
}