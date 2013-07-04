﻿using System.Collections.Generic;
using LondonUbfWebDrive.Domain.Model;

namespace LondonUbfWebDrive.Domain.Services
{
    public class BreadcrumbService : IBreadcrumbService
    {
        public IEnumerable<Breadcrumb> ConvertFrom(string path)
        {
            var breadcrumbs = new List<Breadcrumb> {new Breadcrumb("Home", string.Empty)};

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