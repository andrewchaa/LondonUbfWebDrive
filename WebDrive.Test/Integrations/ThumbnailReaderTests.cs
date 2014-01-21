using System.Collections.Generic;
using System.IO;
using System.Linq;
using LondonUbfWebDrive.Test.TestDoubles;
using Machine.Specifications;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;
using WebDrive.Domain.Services;

namespace LondonUbfWebDrive.Test.Integrations
{
    [Subject(typeof(ThumbnailsReader))]
    public class ThumbnailReaderTests
    {
        private static ThumbnailsReader _reader;
        private static IEnumerable<Thumbnail> _thumbnails;
        private static string _pictureDirectory;
        private static string _subddirectory;
        private static FakeConfig _config;
        private static IFileDirectoryService _fileDirectoryService;

        Establish context = () =>
            {
                _fileDirectoryService = new FileDirectoryService();
                _pictureDirectory = @"c:\temp\";
                _subddirectory = "subddirectory 1";
                _config = new FakeConfig();
                _config.PictureDirectory = _pictureDirectory;
                new DirectoryInfo(_pictureDirectory).CreateSubdirectory(_subddirectory);

                _reader = new ThumbnailsReader(_fileDirectoryService, _config);
            };

        Because of = () => _thumbnails = _reader.List(_pictureDirectory);

        It should_return_relative_path_for_the_folder = () => _thumbnails.First().RelativePath.ShouldEqual(_subddirectory);
    }
}
