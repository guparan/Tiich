using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiichDAL;
using Utils;
using TiichService.Interface;
using TiichService.Service;
using System.Web.Security;

namespace Tiich.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult SignIn(User user)
        {
            ErrorHandler eh = new ErrorHandler();
           
            IService<User> service = new UserService();

            if(Request["password2"] != null && user.Password != null)
            {
                if (!user.Password.Equals(Request["password2"]))
                    eh.addError("Les mot des passes ne correspondent pas");
            }
            
            service.Add(user, eh);

            TempData["errors"] = eh.getErrors();

            if(!eh.hasErrors())
            {
                bool rememberMe = Request["rememberMe2"].StartsWith("true") ? true : false; 
                FormsAuthentication.SetAuthCookie(user.Email, rememberMe);
                TempData["success"] = "Bienvenue !";
                return RedirectToAction("Index", "Home");
            }

            user.Password = String.Empty;
            return View("Index", user);
        }

        public ActionResult LogIn(User user)
        {
            UserService service = new UserService();

            if(service.IsValid(user))
            {
                bool rememberMe = Request["rememberMe"].StartsWith("true") ? true : false; 
                FormsAuthentication.SetAuthCookie(user.Email,  rememberMe);
                TempData["success"] = "Bienvenue !";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["errors"] = "Combinaision email/mot de passe invalie.";
                return View(user);
            }
            

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            TempData["success"] = "Bye !";
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(new User());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

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
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

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
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

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
