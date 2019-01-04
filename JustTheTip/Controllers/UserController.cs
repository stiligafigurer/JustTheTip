using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustTheTip.Controllers {
    [Authorize]
    public class UserController : Controller {
        // GET: Profile
        public ActionResult Index() {
            var profileContext = new UserDbContext();
            var userId = User.Identity.GetUserId();
            var currentProfile = profileContext.Users.FirstOrDefault(p => p.UserId == userId);

            return View(new UserModel {
                FirstName = currentProfile?.FirstName,
                LastName = currentProfile?.LastName,
                Gender = currentProfile?.Gender,
                SexualOrientation = currentProfile?.SexualOrientation,
                BirthDate = currentProfile?.BirthDate.Value,
                ProfilePicUrl = currentProfile?.ProfilePicUrl,
                ZodiacSign = currentProfile?.ZodiacSign,
                Country = currentProfile?.Country,
                ActiveUser = currentProfile?.ActiveUser
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model) {
            var profileContext = new UserDbContext();
            var userId = User.Identity.GetUserId();
            var currentProfile = profileContext.Users.FirstOrDefault(p => p.UserId == userId);

            if (currentProfile == null) {
                profileContext.Users.Add(new UserModel {
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    SexualOrientation = model.SexualOrientation,
                    BirthDate = model.BirthDate.Value,
                    ProfilePicUrl = model.ProfilePicUrl,
                    ZodiacSign = model.ZodiacSign,
                    Country = model.Country,
                    ActiveUser = model.ActiveUser
                });
            } else {
                currentProfile.UserId = userId;
                currentProfile.FirstName = model.FirstName;
                currentProfile.LastName = model.LastName;
                currentProfile.Gender = model.Gender;
                currentProfile.SexualOrientation = model.SexualOrientation;
                currentProfile.BirthDate = model.BirthDate;
                currentProfile.ProfilePicUrl = model.ProfilePicUrl;
                currentProfile.ZodiacSign = model.ZodiacSign;
                currentProfile.Country = model.Country;
                currentProfile.ActiveUser = model.ActiveUser;
            }
            profileContext.SaveChanges();

            return RedirectToAction("Index", "User");
        }
    }
}