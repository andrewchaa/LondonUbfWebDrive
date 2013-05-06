using System.Collections.Generic;

namespace LondonUbfWebDrive.Domain
{
    public interface IBreadcrumbMaker
    {
        IEnumerable<Breadcrumb> Make(string path);
    }
}