using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tiich.Controllers
{
    public class ModerationController : Controller
    {
        //
        // GET: /Moderation/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialAjaxTest(string text)
        {
            return View("PartialAjaxTest", text);
        }

        public ActionResult GlobalTagsStatistics()
        {
            return View();
        }
    }
}
