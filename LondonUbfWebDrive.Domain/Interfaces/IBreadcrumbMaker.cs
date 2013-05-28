using System.Collections.Generic;

namespace LondonUbfWebDrive.Domain.Interfaces
{
    public interface IBreadcrumbMaker
    {
        IEnumerable<Breadcrumb> Make(string path);
    }
}