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

        public WebEntity(string name, string fullName) : this(name, fullName, string.Empty) {}
        public WebEntity(string name, string fullName, string extension)
        {
            Name = name;
            FullName = fullName;
            Extension = extension;
        }

    }
}
