using WebDrive.Domain.Contracts;

namespace WebDrive.Controllers
{
    public class FileDocumentsController : DocumentsController
    {
        public FileDocumentsController(IReadDocumentService service, IConfig config)
            : base(service)
        {
            BaseDirectory = config.FileDirectory;
        }
    }
}
