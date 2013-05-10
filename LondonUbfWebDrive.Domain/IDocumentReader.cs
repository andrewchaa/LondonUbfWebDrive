using System.Collections.Generic;
using System.IO;

namespace LondonUbfWebDrive.Domain
{
    public interface IDocumentReader
    {
        IEnumerable<Document> List(string baseFolder, string path);
        IEnumerable<Document> List(string baseFolder);
        byte[] Get(string baseFolder, string path);
    }

}