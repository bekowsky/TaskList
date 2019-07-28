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
       
        public static LinkedList<User> Users = MyHub.Users;
        public ActionResult Info()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;
            return View();
        }
        public ActionResult Find(string id)
        {

            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;
            return View();
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;
            Users.AddLast(new User());
            return View();
        }

        public ActionResult Projects(User user)
        {

            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;
            return View(user);
        }



        public ActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Name, true);
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;

            return RedirectToAction("Info", "Home", user);
        }
        public ActionResult LogOut(User user)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Info", "Home");
        }
        public ActionResult CreateProjects()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;
            var result = "";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            return View();
        }

        public ActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Share = User.Identity.Name;
            return View();
        }
    }
}