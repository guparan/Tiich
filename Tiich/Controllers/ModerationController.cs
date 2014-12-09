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
            TagService service = new TagService();

            List<Tag>[] words = service.GetCommonWords(tag);

            VMTagManager vm = new VMTagManager()
            {
                DirectTags = words[0],
                IndirectTags = words[1]
            };

            return View(vm);
        }

        public ActionResult UpdateActivatedTag()
        {
            string direct = "direct";
            string indirect = "indirect";
            List<int> directIDS = new List<int>();
            List<int> indirectIDS = new List<int>();

            int i = 0;

            while(Request.Params[direct + i] != null)
            {
                directIDS.Add(int.Parse(Request.Params[direct + i]));
                i++;
            }

            i = 0;
            while (Request.Params[indirect + i] != null)
            {
                indirectIDS.Add(int.Parse(Request.Params[indirect + i]));
                i++;
            }

            bool activate = bool.Parse(Request.Params["activate"]);

            List<int> tags = new List<int>();
            tags.AddRange(directIDS);
            tags.AddRange(indirectIDS);

            TagService service = new TagService();
            service.SetTags(directIDS, activate);
            List<Workshop> works = service.GetWorkshopsFromTags(directIDS);
            WorkshopService wService = new WorkshopService();
            works.ForEach(w => wService.AutoTag(w));
            works.ForEach(w => wService.Update(w, w.Tag.ToList()));

            return Redirect("/Moderation/Index");
        }
    }
}
