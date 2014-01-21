using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LondonUbfWebDrive.Test.TestDoubles;
using Machine.Specifications;
using WebDrive.Domain.Model;
using WebDrive.Domain.Services;

namespace LondonUbfWebDrive.Test.Unit
{
    public class ThumbnailsReaderTests
    {
        private static FakeConfig _config = new FakeConfig { PictureDirectory = @"c:\temp\" };
        private static ThumbnailsReader _reader;
        private static FakeFileDirectoryService _fileDirectoryService;
        private static IEnumerable<Thumbnail> _thumbnails;
        private static string _directoryName = "directory";
        private static string _fileName = "file.jpg";
        private static string _fullName = Path.Combine(_config.PictureDirectory, _fileName);

        [Subject(typeof(ThumbnailsReader))]
        public class When_reader_list_thumbnails
        {
            Establish context = () =>
                {
                    _fileDirectoryService = new FakeFileDirectoryService();
                    _fileDirectoryService.Directories = 
                        new List<WebEntity> { WebEntity.Directory(_directoryName, Path.Combine(_config.PictureDirectory, _directoryName))};
                    _fileDirectoryService.Files = 
                        new List<WebEntity> { WebEntity.File(_fileName, _fullName, ".jpg")};
                    _reader = new ThumbnailsReader(_fileDirectoryService, _config);
                };

            Because of = () =>_thumbnails = _reader.List();

            It should_list_a_direcotyr = () => _thumbnails.First().Fullname.ShouldEqual(Path.Combine(_config.PictureDirectory, _directoryName));
            It should_list_a_file = () => _thumbnails.ToList()[1].Fullname.ShouldEqual(Path.Combine(_config.PictureDirectory, _fileName));
        }

        [Subject(typeof(ThumbnailsReader))]
        public class When_reader_reads_an_image_file
        {
            Establish context = () =>
                {
                    _imageEntity = WebEntity.File(_fileName, _fullName, ".jpg");
                    
                    _fileDirectoryService = new FakeFileDirectoryService
                        {
                            File = _imageEntity,
                            Thumbnailimage = _imageContent
                        };
                    _reader = new ThumbnailsReader(_fileDirectoryService, _config);
                };

            Because of = () => _thumbnail = _reader.Get(Path.Combine(_config.PictureDirectory, _fileName));

            It should_have_a_thumbnail_with_name = () => _thumbnail.Name.ShouldEqual(_fileName);
            It should_have_a_thumbnail_with_full_name = () => _thumbnail.Fullname.ShouldEqual(_fullName);
            It should_have_a_thumbnail_with_image_content = () => _thumbnail.Content.ShouldEqual(_imageContent);
            
            private static Thumbnail _thumbnail;
            private static WebEntity _imageEntity;
            private static byte[] _imageContent = Convert.FromBase64String("aW1hZ2U=");
        }
    }
}
