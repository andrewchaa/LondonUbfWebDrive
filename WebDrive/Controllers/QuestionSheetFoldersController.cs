using WebDrive.Domain.Contracts;

namespace WebDrive.Controllers
{
    public class QuestionSheetFoldersController : FoldersControllerBase
    {
        public QuestionSheetFoldersController(IReadDocumentService service, IConfig config)
            : base(service)
        {
            BaseDirectory = config.QuestionSheetDirectory;
        }

    }
}