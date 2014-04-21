using WebDrive.Domain.Contracts;

namespace WebDrive.Controllers
{
    public class QuestionSheetDocumentsController : DocumentsController
    {
        public QuestionSheetDocumentsController(IReadDocumentService service, IConfig config)
            : base(service)
        {
            BaseDirectory = config.QuestionSheetDirectory;
        }
    }
}