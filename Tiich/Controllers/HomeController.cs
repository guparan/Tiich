using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiich.ViewModels;
using TiichDAL;
using TiichService.Service;
using Enums;
using Utils;

namespace Tiich.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index()
        {
            VMSearchResultDisplay vm = new VMSearchResultDisplay();
            string research = Request["research"];
            
            if(!String.IsNullOrEmpty(research))
            {
                WorkshopService service = new WorkshopService();
                vm.VMWorshops = new List<VMWorkshop>();

                //Recherche directe
                List<Workshop> directWS = service.StraightSearch(research, ResearchEnums.ResearchOption.Or);
                VMWorkshop direct = new VMWorkshop()
                {
                    Category = "Recherche direct",
                    Workshops = directWS
                };
                vm.VMWorshops.Add(direct);

                //Recherche indirecte 
                List<Workshop> indirectWS = service.IndirectSearch(research, ResearchEnums.ResearchOption.Or);
                WorkshopHelper.Diff(indirectWS, directWS);
                VMWorkshop indirect = new VMWorkshop()
                {
                    Category = "Résultat de la recherche",
                    Workshops = indirectWS
                };
                vm.VMWorshops.Add(indirect);

                //Recherche étendue
                if (User.Identity.IsAuthenticated)
                {
                    List<Workshop> favoriteWS = service.FavoriteSearch(User.Identity.Name, research, ResearchEnums.ResearchOption.Or);

                    WorkshopHelper.Diff(favoriteWS, directWS);
                    WorkshopHelper.Diff(favoriteWS, indirectWS);

                    VMWorkshop favorite = new VMWorkshop()
                    {
                        Category = "Vous aimerez aussi",
                        Workshops = favoriteWS
                    };
                    vm.VMWorshops.Add(favorite);
                }



                TempData["research"] = research;
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        //
        // GET: /Home/

        public ActionResult Index(VMSearchResultDisplay vm = null)
        {
            if(VMSearchResultDisplay.IsNullOrEmpty(vm))
            {
                WorkshopService wsservice = new WorkshopService();
                vm = new VMSearchResultDisplay();
                vm.ResultType = "Derniers ajouts";

                vm.VMWorshops = new List<VMWorkshop>();

                VMWorkshop vmws = new VMWorkshop();
                vmws.Category = "Dernier ajouts";
                vmws.Workshops = wsservice.GetLast(5);

                vm.VMWorshops.Add(vmws);
            
            }
            
            return View(vm);
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
