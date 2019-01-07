using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JustTheTip.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(ProfileViewModel model)
        {
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                var userContext = new UserDbContext();
                var friendContext = new FriendsDbContext();
                var user = userContext.Users.FirstOrDefault(u => u.UserId == userId);
                List<FriendsModel> friendList = friendContext.Friends.Where(f => f.UserId == userId).ToList();
                var UserDict = new Dictionary<UserModel, string>();
                foreach (var item in friendList)
                {
                    UserDict.Add(userContext.Users.FirstOrDefault(u => u.UserId == item.FriendId), item.Category);
                }

                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Gender = user.Gender;
                model.SexualOrientation = user.SexualOrientation;
                model.ZodiacSign = user.ZodiacSign;
                model.ProfilePicUrl = user.ProfilePicUrl;
                model.BirthDate = user.BirthDate;
                model.Country = user.Country;
                model.Friends = UserDict;

                return View(model);
            }
            return View("~/Views/Home/index.cshtml");
        }
    }
}