using System.Collections.Generic;
using LondonUbfWebDrive.Domain.Model;

namespace LondonUbfWebDrive.Domain.Services
{
    public interface IReadDocumentService
    {
        IEnumerable<Document> List(string baseFolder, string path);
        IEnumerable<Document> List(string baseFolder);
        Document Get(string fullname);
    }

}