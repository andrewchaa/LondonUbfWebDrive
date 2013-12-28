using System.Web.Helpers;
using System.Web.Mvc;

namespace WebDrive.Controllers
{
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

        // GET api/thumbnails/5
        public ActionResult GetThumbnail(string path)
        {
            var image = new WebImage(@"C:\Users\andrew\Documents\Projects\WebDrive\WebDrive\Images\Desert.jpg")
                .Resize(100, 100, true, true)
                .GetBytes();

            return File(image, "image/jpeg");
        }


    }
}
