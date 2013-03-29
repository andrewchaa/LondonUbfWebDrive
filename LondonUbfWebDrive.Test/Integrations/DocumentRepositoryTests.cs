using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Repositories;
using Machine.Specifications;

namespace LondonUbfWebDrive.Test.Integrations
{
    public class DocumentRepositoryTests
    {
        protected static DirectoryInfo Directory;
        protected static IEnumerable<Document> Documents;
        protected static Document Document;
        protected static IDocumentRepository Repository;
        protected static string MyDocument;
        protected static byte[] DocumentBytes;

        Establish context = () => MyDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    }

    [Subject(typeof(Document))]
    public class When_it_reads_directories_in_the_given_directory : DocumentRepositoryTests
    {
        Establish context = () => Repository = new DocumentRepository(); 
        Because it_reads_the_directory = () =>
            {
                Documents = Repository.List(MyDocument);
            };

        It should_have_the_non_empty_file_list = () => Documents.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_files_in_the_given_directory : DocumentRepositoryTests
    {
        Establish context = () => Repository = new DocumentRepository();
        Because It_reads_the_files = () => Documents = Repository.List(MyDocument);

        It should_have_the_list_of_files = () => Documents.Where(d => !d.IsDirectory).ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_a_document : DocumentRepositoryTests
    {
        Establish context = () => Repository = new DocumentRepository();
        Because It_reads_file_as_document = () => Document = Repository.List(MyDocument).First();

        It should_have_document_name = () => Document.Name.ShouldNotBeEmpty();
        It should_have_document_full_name = () => Document.FullName.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_I_download_a_document : DocumentRepositoryTests
    {
        static string _path;
        Establish context = () =>
            {
                _path = Path.Combine(MyDocument, "test.txt");
                File.WriteAllText(_path, "Hello world!");
                Repository = new DocumentRepository();
            };

        Because It_reads_a_file_from_file_system = () => DocumentBytes = Repository.Get(_path);

        It should_have_the_document_in_bytes_array = () => DocumentBytes.ShouldNotBeEmpty();
    }
}
