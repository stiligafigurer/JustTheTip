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
    public class ProfileController : Controller {
        // GET: Profile
        public ActionResult Index() {
            var profileContext = new ProfileDbContext();
            var userId = User.Identity.GetUserId();
            var currentProfile = profileContext.Profiles.FirstOrDefault(p => p.UserId == userId);

            if (currentProfile == null) {
                profileContext.Profiles.Add(new ProfileModel {
                    UserId = userId,
                    FirstName = currentProfile.FirstName,
                    LastName = currentProfile.LastName,
                    Gender = currentProfile.Gender,
                    SexualOrientation = currentProfile.SexualOrientation,
                    BirthDate = currentProfile.BirthDate,
                    ProfilePicUrl = currentProfile.ProfilePicUrl,
                    ZodiacSign = currentProfile.ZodiacSign,
                    Country = currentProfile.Country,
                    ActiveUser = currentProfile.ActiveUser
                });
            }

            return View(new ProfileModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileModel model) {
            var profileContext = new ProfileDbContext();
            var userId = User.Identity.GetUserId();
            var currentProfile = profileContext.Profiles.FirstOrDefault(p => p.UserId == userId);

            if (currentProfile == null) {
                profileContext.Profiles.Add(new ProfileModel {
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    SexualOrientation = model.SexualOrientation,
                    BirthDate = model.BirthDate,
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

            try {
                profileContext.SaveChanges();
            } catch (DbEntityValidationException ex) {
                foreach (var entityValidationErrors in ex.EntityValidationErrors) {
                    foreach (var validationError in entityValidationErrors.ValidationErrors) {
                        Debug.WriteLine("Property: " + validationError.PropertyName + "Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}