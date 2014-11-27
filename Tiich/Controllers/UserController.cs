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
        public ActionResult EditAvatar(HttpPostedFileBase file)
        {
            string baseUrl = "~/Content/Avatars/";
            UserService service = new UserService();
            ErrorHandler eh = new ErrorHandler();
            User user = service.GetUserByName(User.Identity.Name);

            try
            {
                /*Geting the file name*/
                string filename = System.IO.Path.GetFileName(file.FileName);
                /*Saving the file in server folder*/
                file.SaveAs(Server.MapPath(baseUrl + this.Request["ID"] + ".jpeg"));

                user.Avatar = user.ID.ToString() + ".jpeg";
                service.Edit(user, eh);

                ViewBag.Message = "File Uploaded successfully.";
            }
            catch
            {
                ViewBag.Message = "Error while uploading the files.";
            }
            return Account(user.ID);
        }

        public ActionResult Index()
        {
            return View(new User());
        }
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

        [HttpGet]
        public ActionResult Account(int id = -1)
        {
            UserService service = new UserService();
            User user = new User();
            if (User.Identity.IsAuthenticated)
                user = service.GetUserByName(User.Identity.Name);
            else
                user = null;

            return View(user);
        }

        [HttpPost]
        public ActionResult Account(User user)
        {
            UserService service = new UserService();
            ErrorHandler eh = new ErrorHandler();


            User dbUser = service.GetUserByName(user.Email);
            
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Birthday = user.Birthday;
            dbUser.Bio = user.Bio;
            dbUser.Phone = user.Phone;
            
            service.Edit(dbUser, eh);

            return View(dbUser);
        }
    }
}
