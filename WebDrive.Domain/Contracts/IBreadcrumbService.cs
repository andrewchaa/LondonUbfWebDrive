using System.Collections.Generic;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Contracts
{
    public interface IBreadcrumbService
    {
        IEnumerable<Breadcrumb> ConvertFrom(string path);
    }
}