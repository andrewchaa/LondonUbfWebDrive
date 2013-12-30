using System.Collections.Generic;
using WebDrive.Domain.Model;

namespace WebDrive.Domain.Services
{
    public interface IBreadcrumbService
    {
        IEnumerable<Breadcrumb> ConvertFrom(string path);
    }
}