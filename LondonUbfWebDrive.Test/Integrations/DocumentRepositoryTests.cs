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
        protected static DirectoryInfo Folder;
        protected static IEnumerable<Document> Documents;
        protected static Document Document;
        protected static IDocumentRepository Repository;
        protected static string BaseFolder;
        protected static string CurrentPath;
        protected static byte[] DocumentBytes;
        protected static string FilePath;

        Establish context = () =>
            {
                BaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                CurrentPath = "/";
            };

    }

    [Subject(typeof(Document))]
    public class When_it_reads_directories_in_the_given_directory : DocumentRepositoryTests
    {
        Establish context = () => Repository = new DocumentRepository(); 
        Because it_reads_the_directory = () =>
            {
                Documents = Repository.List(BaseFolder, CurrentPath);
            };

        It should_have_the_non_empty_file_list = () => Documents.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_a_sub_folder: DocumentRepositoryTests
    {
        static string _tempPath;
        Establish context = () =>
            {
                _tempPath = Path.Combine(BaseFolder, "Temp");
                Directory.CreateDirectory(_tempPath);
                Repository = new DocumentRepository();
            }; 

        Because it_reads_a_sub_folder = () => Documents = Repository.List(BaseFolder, CurrentPath);

        It should_have_the_base_folder_as_parent_directory = () => Documents.ShouldContain(d => d.Name == "...");

        Cleanup delete_the_temp_folder = () => Directory.Delete(_tempPath);
    }

    [Subject(typeof(Document))]
    public class When_it_reads_files_in_the_given_directory : DocumentRepositoryTests
    {
        Establish context = () => Repository = new DocumentRepository();
        Because It_reads_the_files = () => Documents = Repository.List(BaseFolder, CurrentPath);

        It should_have_the_list_of_files = () => Documents.Where(d => !d.IsFolder).ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_it_reads_a_document : DocumentRepositoryTests
    {
        Establish context = () => Repository = new DocumentRepository();
        Because It_reads_file_as_document = () => Document = Repository.List(BaseFolder, CurrentPath).Skip(1).First();

        It should_have_document_name = () => Document.Name.ShouldNotBeEmpty();
        It should_have_document_full_name = () => Document.FullName.ShouldNotBeEmpty();
        It should_have_date_modified = () => Document.DateModified.ShouldNotBeEmpty();
    }

    [Subject(typeof(Document))]
    public class When_I_download_a_document : DocumentRepositoryTests
    {
        Establish context = () =>
            {
                FilePath = Path.Combine(BaseFolder, "test.txt");
                File.WriteAllText(FilePath, "Hello world!");
                Repository = new DocumentRepository();
            };

        Because It_reads_a_file_from_file_system = () => DocumentBytes = Repository.Get(BaseFolder, FilePath);

        It should_have_the_document_in_bytes_array = () => DocumentBytes.ShouldNotBeEmpty();
    }

}
