using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Tiich.ViewModels;
using TiichDAL;
using TiichService.Service;

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
            VMTagsStatistics vm = new VMTagsStatistics();

            //Get tags
            TagService tagService = new TagService();
            List<Tag> tags = tagService.GetMostUsed(7);
            
            //Consolidate VM
            vm.CreateTagList(tags);

            return View(vm);
        }

        public ActionResult TopAndFlopWorkshops()
        {
            WorkshopService service = new WorkshopService();
            List<Workshop> workshops = new List<Workshop>();

            workshops.AddRange(service.GetTopViewed(5).ToList());
            workshops.AddRange(service.GetFlopViewed(5).ToList());

            return View(workshops);
        }

        public ActionResult GetCommonWords(string tag)
        {
            List<string> words = new List<string>();

            return View(words);
        }
    }
}
