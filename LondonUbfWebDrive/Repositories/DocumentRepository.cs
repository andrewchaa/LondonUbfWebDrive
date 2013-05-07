﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LondonUbfWebDrive.Domain;

namespace LondonUbfWebDrive.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public IEnumerable<Document> List(string baseFolder, string path)
        {

            var directory = new DirectoryInfo(Path.Combine(baseFolder, path));

            var documents = new List<Document>();
            documents.AddRange(GetFolders(baseFolder, directory));
            documents.AddRange(GetFiles(baseFolder, directory));

            return documents;
        }

        public IEnumerable<Document> List(string baseFolder)
        {
            return List(baseFolder, string.Empty);
        }

        public byte[] Get(string baseFolder, string path)
        {
            return File.ReadAllBytes(Path.Combine(baseFolder, path));
        }

        private IEnumerable<Document> GetFiles(string baseFolder, DirectoryInfo directory)
        {
            return directory.GetFiles().Select(file => new Document(
                file.Name, 
                file.FullName.Replace(baseFolder, string.Empty), 
                file.LastWriteTimeUtc.ToShortDateString(), 
                false, 
                GetFileImage(file.Extension))).ToList();
        }

        private string GetFileImage(string extension)
        {
            string image = "document.png";
            switch (extension)
            {
                case ".pdf": 
                    image = "pdf.png";
                    break;
                case ".zip":
                    image = "zip.png";
                    break;
                case ".doc":
                case ".docx":
                    image = "word.png";
                    break;
                case ".xlsx":
                case ".xls":
                    image = "excel.png";
                    break;
                case ".ppt":
                case ".pptx":
                    image = "powerpoint.png";
                    break;
            }
            
            return "/Images/" + image;
        }

        private IEnumerable<Document> GetFolders(string baseFolder, DirectoryInfo directory)
        {
            return directory.GetDirectories()
                            .Select(d => new Document(
                                             d.Name, 
                                             d.FullName.Replace(baseFolder, string.Empty),
                                             d.LastWriteTime.ToString(),
                                             true,
                                             "/Images/folder.png"
                                             ))
                            .ToList();
        }
    }


}