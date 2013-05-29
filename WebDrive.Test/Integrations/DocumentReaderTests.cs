using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Repositories;
using Machine.Specifications;

namespace LondonUbfWebDrive.Test.Integrations
{
    public class DocumentReaderTests
    {
        protected static DirectoryInfo Folder;
        protected static IEnumerable<Document> Documents;
        protected static Document Document;
        protected static IDocumentReader Reader;
        protected static string BaseFolder;
        protected static string CurrentPath;
        protected static byte[] DocumentBytes;
        protected static string FilePath;

        Establish context = () =>
            {
                BaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                CurrentPath = string.Empty;
            };

    }

    [Subject(typeof(Document))]
    public class When_it_reads_directories_in_the_given_directory : DocumentReaderTests
    {
        Establish context = () => Reader = new DocumentReader(); 
        Because it_reads_the_directory = () =>
            {
                Documents = Reader.List(BaseFolder, CurrentPath);
            };

        It should_have_the_non_empty_file_list = () => Documents.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_files_in_the_given_directory : DocumentReaderTests
    {
        Establish context = () => Reader = new DocumentReader();
        Because It_reads_the_files = () => Documents = Reader.List(BaseFolder, CurrentPath);

        It should_have_the_list_of_files = () => Documents.Where(d => !d.IsFolder).ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_I_read_a_directory_that_contains_temp_files : DocumentReaderTests
    {
        private static string _tempFileName;

        Establish context = () =>
            {
                Reader = new DocumentReader();
                _tempFileName = "~temp.docx";
                File.WriteAllText(Path.Combine(BaseFolder, _tempFileName), "Temp file");
            };
        
        Because of = () => Documents = Reader.List(BaseFolder, CurrentPath);

        It should_filter_out_temp_files = () => Documents.Any(d => d.Name == _tempFileName).ShouldBeFalse();
    }
    
    [Subject(typeof(Document))]
    public class When_it_reads_a_document : DocumentReaderTests
    {
        Establish context = () => Reader = new DocumentReader();
        Because It_reads_file_as_document = () => Document = Reader.List(BaseFolder, CurrentPath).Skip(1).First();

        It should_have_document_name = () => Document.Name.ShouldNotBeEmpty();
        It should_have_document_full_name = () => Document.FullName.ShouldNotBeEmpty();
        It should_have_date_modified = () => Document.DateModified.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_I_download_a_document : DocumentReaderTests
    {
        Establish context = () =>
            {
                FilePath = Path.Combine(BaseFolder, "test.txt");
                File.WriteAllText(FilePath, "Hello world!");
                Reader = new DocumentReader();
            };

        Because It_reads_a_file_from_file_system = () => DocumentBytes = Reader.Get(BaseFolder, FilePath);

        It should_have_the_document_in_bytes_array = () => DocumentBytes.ShouldNotBeEmpty();
    }

}
