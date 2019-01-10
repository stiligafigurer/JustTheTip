using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustTheTip.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            var userDbContext = new UserDbContext();
            var userList = new List<UserModel>();
            

            if (Request.IsAuthenticated) {
                var user = User.Identity.GetUserId();
                userList.AddRange(userDbContext.Users.Where(u => u.UserId != user));
                return View("Index_LoggedIn", userList);
            } else {
               
                userList.AddRange(userDbContext.Users);
                return View("Index", userList);
            }

        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Index_LoggedIn(List<UserModel> userList) {
            return View(userList);
        }
    }
}