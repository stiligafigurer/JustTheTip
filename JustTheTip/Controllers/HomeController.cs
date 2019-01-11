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
            var userContext = new UserDbContext();
            var userList = new List<UserModel>();
            
            if (Request.IsAuthenticated) {
                var userId = User.Identity.GetUserId();
                userList.AddRange(userContext.Users.Where(u => u.UserId != userId && u.ActiveUser == 1));
                var currentUser = userContext.Users.FirstOrDefault(u => u.UserId == userId);
                if(currentUser.ActiveUser == 0) {
                    currentUser.ActiveUser = 1;
                    userContext.SaveChanges();
                    TempData["alertMessage"] = "Your account is now reactivated. Welcome back!";
                    return View("Index_LoggedIn", userList);
                }
                return View("Index_LoggedIn", userList);
            } else {
                userList.AddRange(userContext.Users.Where(u => u.ActiveUser == 1));
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