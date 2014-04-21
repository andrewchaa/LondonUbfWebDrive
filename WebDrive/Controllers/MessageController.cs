using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDrive.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Message/

        public ActionResult Index()
        {
            return View();
        }

    }

    public class QuestionSheetController : Controller
    {
        //
        // GET: /questionsheet/

        public ActionResult Index()
        {
            return View();
        }

    }
}
