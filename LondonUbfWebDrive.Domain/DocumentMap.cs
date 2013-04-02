using System.Collections.Generic;

namespace LondonUbfWebDrive.Domain
{
    public class DocumentMap
    {
        public IEnumerable<Document> Documents { get; private set; } 
        public IEnumerable<Breadcrumb> Breadcrumbs { get; private set; } 

        public DocumentMap(IEnumerable<Breadcrumb> breadcrumbs, IEnumerable<Document> documents)
        {
            Breadcrumbs = breadcrumbs;
            Documents = documents;
        }
    }
}