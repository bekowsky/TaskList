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
        int j = MyHub.hui;
        public static LinkedList<User> Users = MyHub.Users;
        public ActionResult Index()
        {
            Users.AddLast(new Models.User());
            return View();
        }

        public ActionResult Projects()
        {
           
            return View();
        }

        public ActionResult CreateProjects()
        {
            var result = "";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}