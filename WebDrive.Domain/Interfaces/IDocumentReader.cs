using System.Collections.Generic;

namespace LondonUbfWebDrive.Domain.Interfaces
{
    public interface IDocumentReader
    {
        IEnumerable<Document> List(string baseFolder, string path);
        IEnumerable<Document> List(string baseFolder);
        byte[] Get(string baseFolder, string path);
    }

}