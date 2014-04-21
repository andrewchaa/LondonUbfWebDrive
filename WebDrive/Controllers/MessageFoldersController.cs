using WebDrive.Domain.Contracts;

namespace WebDrive.Controllers
{
    public class MessageFoldersController : FoldersControllerBase
    {
        public MessageFoldersController(IReadDocumentService service, IConfig config) : base(service)
        {
            BaseDirectory = config.MessageDirectory;
        }

    }
}
