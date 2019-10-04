using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskList.Hubs;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class HomeController : Controller
    {
       

        TaskContext db = new TaskContext();
        public ActionResult Info()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = db.FindByName(User.Identity.Name);


            return View();
        }
        public ActionResult Find(string id)
        {
           
            if (User.Identity.IsAuthenticated)
                ViewBag.Share =db.FindByName(User.Identity.Name);
            Project model = db.FindByKey(id);
            return View(model);
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = db.FindByName(User.Identity.Name);
            User user = db.FindByName(User.Identity.Name);

            return View();
        }

        public ActionResult Projects()
        {

            if (User.Identity.IsAuthenticated)
                ViewBag.Share = db.FindByName(User.Identity.Name);

            User user = db.FindByName(User.Identity.Name);

            return View(user);
        }



        public ActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = db.FindByName(User.Identity.Name);
            return View();
        }

        public ActionResult SignUp()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user,string Gender)
        {
            if (user != null)
            {
                if (Gender == "man")
                    user.IsMale = true;

                user.Date = DateTime.Now;
                user.WasOnline = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.Name, true);
                    return RedirectToAction("Info", "Home");
                }
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            if (db.IsCorrect(user.Name, user.Password))
            {             
                FormsAuthentication.SetAuthCookie(user.Name, true);
                return RedirectToAction("Info", "Home");
            }
            return View(user);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Info", "Home");
        }
        public ActionResult CreateProjects(string key)
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = db.FindByName(User.Identity.Name);
            //Project project = db.FindByKey(key);
            return RedirectToAction("Find", "Home", key);
            
        }

        public ActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = db.FindByName(User.Identity.Name);
            return View();
        }

     
    }
}