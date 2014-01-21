using System.Collections.Generic;
using System.IO;
using System.Linq;
using LondonUbfWebDrive.Test.TestDoubles;
using Machine.Specifications;
using WebDrive.Controllers;
using WebDrive.Domain.Model;

namespace LondonUbfWebDrive.Test.Unit
{
    public class FileDirectoryControllerTests
    {
        [Subject(typeof(FileDirectoryController))]
        public class When_you_requeset_without_path
        {
            Establish context = () =>
                {
                    _controller = new FileDirectoryController(_config, _fileDirectoryService);
                };

            Because of = () => _entities = _controller.Get();

            It should_return_directories = () => _entities.First().IsDirectory.ShouldBeTrue();

            private static FileDirectoryController _controller;
            private static IEnumerable<WebEntity> _entities;
            private static FakeFileDirectoryService _fileDirectoryService = 
                new FakeFileDirectoryService
                    {
                        Directories = new [] { new WebEntity(_directoryName, _directoryFullName) },
                        Files = new[] { new WebEntity(_fileName, _fileFullName, ".jpg") }
                    };
            private static FakeConfig _config = new FakeConfig {PictureDirectory = @"c:\temp\"};
            private static string _directoryName = "Directory";
            private static string _directoryFullName = Path.Combine(_config.PictureDirectory, _directoryName);
            private static string _fileName = "File";
            private static string _fileFullName = Path.Combine(_config.PictureDirectory, _fileName);
        }
    }
}
