using WebDrive.Domain.Contracts;

namespace WebDrive.Controllers
{
    public class MessageDocumentsController : DocumentsController
    {
        public MessageDocumentsController(IReadDocumentService service, IConfig config)
            : base(service)
        {
            BaseDirectory = config.MessageDirectory;
        }
    }
}
