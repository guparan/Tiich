using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiichDAL;
using TiichService.Service;
using Utils;

namespace Tiich.Controllers
{
    public class WorkshopController : Controller
    {
        [HttpGet]
        public ActionResult Create(int id = -1)
        {
            Workshop ws = new Workshop();
            WorkshopService service = new WorkshopService();
            UserService userService = new UserService();

            if(id != -1)
            {
                ws = service.Find(id);
            }

            if(User.Identity.IsAuthenticated)
            {
                TempData["UserID"] = userService.GetUserByName(User.Identity.Name).ID;
            }
            
            if(id != -1 && User.Identity.IsAuthenticated)
            {
                service.AddVisitorToWorkshop(TempData["UserID"].ToString(), id);
                //int userID = int.Parse(TempData["UserID"]);
                //if(userID != )
            }
        
            return View(ws);
        }

        [HttpPost]
        public ActionResult Create(Workshop ws)
        {
            UserService userService = new UserService();
            WorkshopService service = new WorkshopService();
            ErrorHandler eh = new ErrorHandler();
            User user = userService.GetUserByName(User.Identity.Name);

            if(ws.ID == 0)
            {
                //Consolidation
                ws.User = user;
                ws.CreationDate = DateTime.Now;

                service.Add(ws, eh, new List<object>() { user });

            }
            else
            {
                ws.UserID = user.ID;
                service.Edit(ws, eh);
            }

            if (User.Identity.IsAuthenticated)
                TempData["UserID"] = userService.GetUserByName(User.Identity.Name).ID; 
            
            //return View(ws);
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Participate()
        {
            int userID = int.Parse(Request["userID"]);
            int wsID = int.Parse(Request["workshopID"]);

            WorkshopService service = new WorkshopService();
            service.AddParticipant(userID, wsID);

            return RedirectToAction("Create", new { @id = wsID });
        }

        public ActionResult UnParticipate()
        {
            int userID = int.Parse(Request["userID"]);
            int wsID = int.Parse(Request["workshopID"]);

            WorkshopService service = new WorkshopService();
            service.RemoveParticipant(userID, wsID);

            return RedirectToAction("Create", new { @id = wsID });
        }
        
    }
}
