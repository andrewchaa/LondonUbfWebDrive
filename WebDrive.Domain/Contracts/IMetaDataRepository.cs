using System.Collections.Generic;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Contracts
{
    public interface IMetaDataRepository
    {
        void Save(DocumentMetadata documentMetadata);
        IEnumerable<DocumentMetadata> List();
        IEnumerable<DocumentMetadata> ListPopular();
        IEnumerable<DocumentMetadata> ListRecentDownloads();
    }
}