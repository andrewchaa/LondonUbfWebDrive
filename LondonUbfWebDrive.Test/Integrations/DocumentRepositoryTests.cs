using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LondonUbfWebDrive.Domain;
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

        Establish context = () => MyDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    }

    [Subject(typeof(Document))]
    public class When_it_reads_directories_in_the_given_directory : DocumentRepositoryTests
    {
        private Establish context = () => Repository = new DocumentRepository(); 
        private Because it_reads_the_directory = () =>
            {
                Documents = Repository.Read(MyDocument);
            };

        private It should_have_the_non_empty_file_list = () => Documents.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_files_in_the_given_directory : DocumentRepositoryTests
    {
        private Establish context = () => Repository = new DocumentRepository();
        private Because It_reads_the_files = () => Documents = Repository.Read(MyDocument);

        private It should_have_the_list_of_files = () => Documents.Where(d => !d.IsDirectory).ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_a_document : DocumentRepositoryTests
    {
        private Establish context = () => Repository = new DocumentRepository();
        private Because It_reads_file_as_document = () => Document = Repository.Read(MyDocument).First();

        private It should_have_document_name = () => Document.Name.ShouldNotBeEmpty();
        private It should_have_document_full_name = () => Document.FullName.ShouldNotBeEmpty();
    }
}
