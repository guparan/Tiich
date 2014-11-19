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
            
            if(id != -1)
            {

            }

            return View(ws);
        }

        [HttpPost]
        public ActionResult Create(Workshop ws)
        {
            UserService userService = new UserService();
            WorkshopService service = new WorkshopService();
            ErrorHandler eh = new ErrorHandler();

            //Consolidation
            User user = userService.GetUserByName(User.Identity.Name);
            ws.User = user;
            ws.CreationDate = DateTime.Now;

            service.Add(ws, eh, new List<object>(){user});

            return View(ws);
        }
    }
}
