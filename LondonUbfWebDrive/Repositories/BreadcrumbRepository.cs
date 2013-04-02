using System;
using System.Collections.Generic;
using LondonUbfWebDrive.Domain;

namespace LondonUbfWebDrive.Repositories
{
    public class BreadcrumbRepository : IBreadcrumbRepository
    {
        private readonly string _baseFolder;

        public BreadcrumbRepository(string baseFolder)
        {
            _baseFolder = baseFolder;
        }

        public IEnumerable<Breadcrumb> List (string path)
        {
            var breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(new Breadcrumb("Home", "\\"));
            if (path == "\\")
                return breadcrumbs;

            var relativePath = path.Replace(_baseFolder, string.Empty).Trim('\\');
            var folders = relativePath.Split('\\');
            
            for (int i = 0; i < folders.Length; i++)
            {
                string folderName = folders[i];
                string folderPath = string.Empty;
                for (int j = 0; j <= i; j++)
                    folderPath += "\\" + folders[j];

                breadcrumbs.Add(new Breadcrumb(folderName, folderPath));
                
            }

            return breadcrumbs;
        }
    }
}