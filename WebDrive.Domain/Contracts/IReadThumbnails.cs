using System.Collections.Generic;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Contracts
{
    public interface IReadThumbnails
    {
        IEnumerable<Thumbnail> List(string path);
        IEnumerable<Thumbnail> List();
    }
}