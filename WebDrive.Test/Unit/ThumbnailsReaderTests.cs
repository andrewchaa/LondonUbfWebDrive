using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using Machine.Specifications.Model;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Services;

namespace LondonUbfWebDrive.Test.Unit
{
    public class ThumbnailsReaderTests
    {
        [Subject(typeof(ThumbnailsReader))]
        public class When_reader_reads_images
        {
            Establish context = () =>
                {
                    _reader = new ThumbnailsReader(_fileDirectoryService, _config);
                };

            Because of = () => _reader.List(@"c:\temp\");

//            It should_first_try_read_images_from_thumbnail_folder = ()

            private static IConfig _config;
            private static ThumbnailsReader _reader;
            private static IFileDirectoryService _fileDirectoryService;
        }
    }
}
