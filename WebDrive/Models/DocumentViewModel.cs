namespace WebDrive.Models
{
    public class DocumentViewModel
    {
        public string FullName { get; set; }
        public bool IsFolder { get; set; }
        public string DateModified { get; set; }
        public string ImagePath { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }

    }
}