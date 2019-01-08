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

        private int CheckCompatibility(string id) {
            var compatibility = 0;
            var userContext = new UserDbContext();
            var userId = User.Identity.GetUserId();

            var currentUser = userContext.Users.FirstOrDefault(u => u.UserId == userId);
            var compareUser = userContext.Users.FirstOrDefault(u => u.UserId == id);

            // Check age compatibility
            if (currentUser.BirthDate.Value.Year <= compareUser.BirthDate.Value.Year + 1 || 
                currentUser.BirthDate.Value.Year >= compareUser.BirthDate.Value.Year - 1) {
                compatibility += 20;

            } else if (currentUser.BirthDate.Value.Year <= compareUser.BirthDate.Value.Year +3 || 
                currentUser.BirthDate.Value.Year >= compareUser.BirthDate.Value.Year -3) {
                compatibility += 10;
            } else if (currentUser.BirthDate.Value.Year <= compareUser.BirthDate.Value.Year + 5 ||
                currentUser.BirthDate.Value.Year >= compareUser.BirthDate.Value.Year - 5) {
                compatibility += 5;
            }

            // Check zodiac sign compatibility
            if (currentUser.ZodiacSign == compareUser.ZodiacSign) {
                compatibility += 20;
            }

            // Check gender & sexual orientation compatibility
            if (currentUser.SexualOrientation == "Heterosexual" && compareUser.SexualOrientation == "Heterosexual" &&
                currentUser.Gender != compareUser.Gender) {
                compatibility += 20;
            } else if ((currentUser.SexualOrientation == "Homosexual" || currentUser.SexualOrientation == "Bisexual") &&
                (compareUser.SexualOrientation == "Homosexual" || compareUser.SexualOrientation == "Bisexual") &&
                currentUser.Gender == compareUser.Gender) {
                compatibility += 20;
            } else if (currentUser.SexualOrientation == "Other" && compareUser.SexualOrientation == "Other") {
                compatibility += 20;
            }

            // Check country compatibility
            if (currentUser.Country == compareUser.Country) {
                compatibility += 20;
            }

            // Check mutual friends compatibility
            var friendsContext = new FriendsDbContext();
            var userFriends = friendsContext.Friends.Where(f => f.UserId == userId);
            var compareFriends = friendsContext.Friends.Where(f => f.UserId == id);
            int mutualFriends = 0;

            foreach (var friend in userFriends) {
                if (compareFriends.Any(f => f.FriendId == friend.FriendId)) {
                    mutualFriends++;
                }
            }

            if (mutualFriends >= 5) {
                compatibility += 20;
            } else if (mutualFriends >= 3) {
                compatibility += 10;
            } else if (mutualFriends >= 1) {
                compatibility += 5;
            }

            return compatibility;
        }
    }
}