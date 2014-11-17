using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiichDAL;

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
            //Workshop ws = new Workshop();

            //if (id != -1)
            /*{

            }*/

            return View(ws);
        }

        //
        // GET: /Workshop/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Workshop/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Workshop/Create

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
        // GET: /Workshop/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Workshop/Edit/5

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
        // GET: /Workshop/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Workshop/Delete/5

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
