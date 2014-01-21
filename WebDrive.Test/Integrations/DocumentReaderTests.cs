using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Machine.Specifications;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;
using WebDrive.Domain.Services;

namespace LondonUbfWebDrive.Test.Integrations
{
    public class DocumentReaderTests
    {
        protected static DirectoryInfo Folder;
        protected static IEnumerable<Document> Documents;
        protected static Document Document;
        protected static IReadDocumentService Service;
        protected static string BaseFolder;
        protected static string CurrentPath;
        protected static Document _document;
        protected static string _filePath;

        Establish context = () =>
            {
                BaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                CurrentPath = string.Empty;
            };

    }

    [Subject(typeof(Document))]
    public class When_it_reads_directories_in_the_given_directory : DocumentReaderTests
    {
        Establish context = () => Service = new ReadDocumentService(); 
        Because it_reads_the_directory = () =>
            {
                Documents = Service.List(BaseFolder, CurrentPath);
            };

        It should_have_the_non_empty_file_list = () => Documents.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_files_in_the_given_directory : DocumentReaderTests
    {
        Establish context = () => Service = new ReadDocumentService();
        Because It_reads_the_files = () => Documents = Service.List(BaseFolder, CurrentPath);

        It should_have_the_list_of_files = () => Documents.Where(d => !d.IsFolder).ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_I_read_a_directory_that_contains_temp_files : DocumentReaderTests
    {
        private static string _tempFileName;

        Establish context = () =>
            {
                Service = new ReadDocumentService();
                _tempFileName = "~temp.docx";
                File.WriteAllText(Path.Combine(BaseFolder, _tempFileName), "Temp file");
            };
        
        Because of = () => Documents = Service.List(BaseFolder, CurrentPath);

        It should_filter_out_temp_files = () => Documents.Any(d => d.Name == _tempFileName).ShouldBeFalse();
    }
    
    [Subject(typeof(Document))]
    public class When_it_reads_a_document : DocumentReaderTests
    {
        Establish context = () => Service = new ReadDocumentService();
        Because It_reads_file_as_document = () => Document = Service.List(BaseFolder, CurrentPath).Skip(1).First();

        It should_have_document_name = () => Document.Name.ShouldNotBeEmpty();
        It should_have_document_full_name = () => Document.FullName.ShouldNotBeEmpty();
        It should_have_date_modified = () => Document.DateModified.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_I_download_a_document : DocumentReaderTests
    {
        Establish context = () =>
            {
                _filePath = Path.Combine(BaseFolder, "test.txt");
                File.WriteAllText(_filePath, "Hello world!");
                Service = new ReadDocumentService();
            };

        Because of = () => _document = Service.Get(_filePath);

        It should_have_the_content_of_the_document = () => _document.Content.ShouldNotBeEmpty();
    }

}
