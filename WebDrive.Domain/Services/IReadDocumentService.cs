using System.Collections.Generic;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Services
{
    public interface IReadDocumentService
    {
        IEnumerable<Document> List(string baseFolder, string path);
        IEnumerable<Document> List(string baseFolder);
        Document Get(string fullname);
    }

}