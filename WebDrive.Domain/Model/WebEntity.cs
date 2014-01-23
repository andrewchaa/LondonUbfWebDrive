using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace WebDrive.Domain.Model
{
    public class WebEntity
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string Extension { get; private set; }
        public byte[] Content { get; private set; }

        public string ContentType { get { return MimeMapping.GetMimeMapping(Name); } }
        public bool IsDirectory { get { return string.IsNullOrEmpty(Extension); } }
        public string FullNameBase64 { get { return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(FullName)); } }

        public bool IsImage
        {
            get
            {
                var imageFileExtensions = new List<string> { ".jpg", ".png", ".gif", ".tif" };
                return imageFileExtensions.Contains(Extension.ToLower());
            }
        }

        public WebEntity(string name, string fullName) : this(name, fullName, string.Empty) {}
        public WebEntity(string name, string fullName, string extension)
        {
            Name = name;
            FullName = fullName;
            Extension = extension;
        }

        public WebEntity(string name, string fullName, string extension, byte[] content)
        {
            Name = name;
            FullName = fullName;
            Extension = extension;
            Content = content;
        }


    }
}
