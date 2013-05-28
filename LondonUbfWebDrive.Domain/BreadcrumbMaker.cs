using System.Collections.Generic;
using System.Linq;
using LondonUbfWebDrive.Domain.Interfaces;

namespace LondonUbfWebDrive.Domain
{
    public class BreadcrumbMaker : IBreadcrumbMaker
    {
        public IEnumerable<Breadcrumb> Make(string path)
        {
            var breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(new Breadcrumb("Home", string.Empty));

            if (string.IsNullOrEmpty(path))
                return breadcrumbs;

            var folderNames = path.Split('/');
            string folderPath = string.Empty;
            foreach (var name in folderNames)
            {
                folderPath += "\\" + name;
                breadcrumbs.Add(new Breadcrumb(name, folderPath));
            }

            return breadcrumbs;
        }
    }
}