using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace JustTheTip.Controllers {
    [Authorize]
    public class UserController : Controller {
        // GET: Profile
        public ActionResult Index() {
            var userContext = new UserDbContext();
            var userId = User.Identity.GetUserId();
            var currentUser = userContext.Users.FirstOrDefault(u => u.UserId == userId);

            return View(new UserModel {
                FirstName = currentUser?.FirstName,
                LastName = currentUser?.LastName,
                Gender = currentUser?.Gender,
                SexualOrientation = currentUser?.SexualOrientation,
                BirthDate = currentUser?.BirthDate.Value,
                ProfilePicUrl = currentUser?.ProfilePicUrl,
                ZodiacSign = currentUser?.ZodiacSign,
                Country = currentUser?.Country,
                ActiveUser = currentUser?.ActiveUser
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model) {
            var userContext = new UserDbContext();
            var userId = User.Identity.GetUserId();
            var currentUser = userContext.Users.FirstOrDefault(u => u.UserId == userId);

            if (currentUser == null) {
                userContext.Users.Add(new UserModel {
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
                currentUser.UserId = userId;
                currentUser.FirstName = model.FirstName;
                currentUser.LastName = model.LastName;
                currentUser.Gender = model.Gender;
                currentUser.SexualOrientation = model.SexualOrientation;
                currentUser.BirthDate = model.BirthDate;
                currentUser.ProfilePicUrl = model.ProfilePicUrl;
                currentUser.ZodiacSign = model.ZodiacSign;
                currentUser.Country = model.Country;
                currentUser.ActiveUser = model.ActiveUser;
            }
            userContext.SaveChanges();

            return RedirectToAction("Index", "User");
        }
    }
}