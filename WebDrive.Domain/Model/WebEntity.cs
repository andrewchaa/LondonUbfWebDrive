using System;
using System.Text;
using System.Web;

namespace WebDrive.Domain.Model
{
    public class WebEntity
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string RelativePath { get; private set; }
        public string Extension { get; private set; }
        
        public bool IsDirectory
        {
            get { return string.IsNullOrEmpty(Extension); }
        }

        public string FullNameBase64 
        { 
            get { return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(FullName)); } 
        }

        public WebEntity(string name, string fullName) : this(name, fullName, string.Empty) {}
        public WebEntity(string name, string fullName, string extension)
        {
            Name = name;
            FullName = fullName;
            Extension = extension;
        }


    }
}
