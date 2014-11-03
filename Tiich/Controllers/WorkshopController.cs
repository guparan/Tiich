using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tiich.Controllers
{
    public class WorkshopController : Controller
    {
        //
        // GET: /Workshop/

        public ActionResult Index()
        {
            return View();
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
