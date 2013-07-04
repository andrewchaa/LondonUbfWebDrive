using System.Collections.Generic;
using LondonUbfWebDrive.Domain.Model;

namespace LondonUbfWebDrive.Domain.Services
{
    public interface IBreadcrumbService
    {
        IEnumerable<Breadcrumb> ConvertFrom(string path);
    }
}