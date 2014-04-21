using WebDrive.Domain.Contracts;

namespace WebDrive.Controllers
{
    public class FileFoldersController : FoldersControllerBase
    {
        public FileFoldersController(IReadDocumentService service, IConfig config) : base(service)
        {
            BaseDirectory = config.FileDirectory;
        }

    }
}
