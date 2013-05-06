using System.Collections.Generic;
using System.Linq;

namespace LondonUbfWebDrive.Domain
{
    public class BreadcrumbMaker : IBreadcrumbMaker
    {
        public IEnumerable<Breadcrumb> Make(string path)
        {
            var breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(new Breadcrumb("Home", "\\"));

            if (string.IsNullOrEmpty(path))
                return breadcrumbs;

            var folderNames = path.Split('/');
            string folderPath = "\\";
            foreach (var name in folderNames)
            {
                folderPath += name + "\\";
                breadcrumbs.Add(new Breadcrumb(name, folderPath));
            }

            return breadcrumbs;
        }
    }
}