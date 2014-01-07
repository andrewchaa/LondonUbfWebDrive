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
        private readonly IFileDirectoryService _fileDirectoryService;
        private readonly IConfig _config;
        private readonly string[] _imageFileExtensions;

        public ThumbnailsReader(IFileDirectoryService fileDirectoryService, IConfig config)
        {
            _fileDirectoryService = fileDirectoryService;
            _config = config;
            _imageFileExtensions = new[] { ".jpg", ".png", ".gif", ".tif" };
        }

        public IEnumerable<Thumbnail> List()
        {
            var thumbnails = GetDirectories(_config.PictureDirectory).ToList();
            thumbnails.AddRange(GetThumbnails(_config.PictureDirectory));

            return thumbnails.ToList();
        }

        public IEnumerable<Thumbnail> List(string relativePath)
        {
            string path = Path.Combine(_config.PictureDirectory, relativePath);

            var thumbnails = GetDirectories(path).ToList();
            thumbnails.AddRange(GetThumbnails(path));

            return thumbnails.ToList();
        }

        private IEnumerable<Thumbnail> GetDirectories(string path)
        {
            var directory = new DirectoryInfo(path);
            return directory.EnumerateDirectories().Select(d =>
                new Thumbnail(d.FullName, d.Name, GetRelativePath(d.FullName), null, string.Empty, true)).ToList();
        }

        private string GetRelativePath(string fullname)
        {
            return fullname.Replace(_config.PictureDirectory, string.Empty);
        }

        private IEnumerable<Thumbnail> GetThumbnails(string path)
        {
            var directory = new DirectoryInfo(path);
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