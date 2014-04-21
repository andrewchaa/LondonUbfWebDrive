using System.Collections.Generic;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Contracts
{
    public interface IReadDocumentService
    {
        IEnumerable<Document> List(string baseFolder, string path);
        Document Get(string fullname);
    }

}