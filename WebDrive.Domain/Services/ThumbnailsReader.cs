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
            var directories = _fileDirectoryService.EnumerateDirectories(path);
            return directories.Select(
                d => new Thumbnail(d.FullName, d.Name, GetRelativePath(d.FullName), null, string.Empty, true)
                ).ToList();
        }

        private string GetRelativePath(string fullname)
        {
            return fullname.Replace(_config.PictureDirectory, string.Empty);
        }

        private IEnumerable<Thumbnail> GetThumbnails(string path)
        {
            var files = _fileDirectoryService.EnumerateFiles(path).Where(f => _imageFileExtensions.Contains(f.Extension.ToLower()));
            var thumbnails = files.Select(t => new Thumbnail(
                                                         t.FullName,
                                                         t.Name,
                                                         GetRelativePath(t.FullName),
//                                                         new WebImage(t.FullName).Resize(100, 100, true, true).GetBytes(),
                                                         MimeMapping.GetMimeMapping(t.Name),
                                                         false)
                                    );
            return thumbnails;
        }

        public Thumbnail Get(string fullName)
        {
            var entity = _fileDirectoryService.GetFile(fullName);
            if (entity.IsDirectory)
                return new Thumbnail(entity.FullName, entity.Name, GetRelativePath(entity.FullName), null, string.Empty, true);

            return new Thumbnail(
                entity.FullName, 
                entity.Name, 
                GetRelativePath(entity.FullName), 
                _fileDirectoryService.GetThumbnailImage(fullName),
                MimeMapping.GetMimeMapping(entity.Name),
                false
                );
        }
    }
}