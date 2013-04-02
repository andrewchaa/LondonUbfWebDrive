using System.Collections.Generic;

namespace LondonUbfWebDrive.Domain
{
    public interface IBreadcrumbRepository
    {
        IEnumerable<Breadcrumb> List(string path);
    }
}