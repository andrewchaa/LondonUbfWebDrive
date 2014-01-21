using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDrive.Domain.Model
{
    public class WebEntity
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string Extension { get; private set; }
        public bool IsDirectory { get; private set; }

        public static WebEntity Directory(string name, string fullName)
        {
            return new WebEntity{Name = name, FullName = fullName, IsDirectory = true};
        }

        public static WebEntity File(string name, string fullName, string extension)
        {
            return new WebEntity{ Name = name, FullName = fullName, Extension = extension};
        }

        private WebEntity() {}

    }
}
