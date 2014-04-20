using System.Web.Helpers;
using System.Web.Mvc;

namespace WebDrive.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

    }
}
