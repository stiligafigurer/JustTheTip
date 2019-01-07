using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

            ViewBag.CountryList = GetList.Countries();
            ViewBag.ZodiacList = GetList.ZodiacSigns();
            ViewBag.GenderList = GetList.Genders();
            ViewBag.SexOrList = GetList.SexualOrientations();

            return View(new UserModel {
                FirstName = currentUser?.FirstName,
                LastName = currentUser?.LastName,
                Gender = currentUser?.Gender,
                SexualOrientation = currentUser?.SexualOrientation,
                BirthDate = (System.DateTime) currentUser?.BirthDate,
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
                currentUser.BirthDate = model.BirthDate.Value;
                currentUser.ProfilePicUrl = model.ProfilePicUrl;
                currentUser.ZodiacSign = model.ZodiacSign;
                currentUser.Country = model.Country;
                currentUser.ActiveUser = model.ActiveUser;
            }
            try {
                userContext.SaveChanges();
            } catch (DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            } catch (System.Data.Entity.Infrastructure.DbUpdateException e) {
                Debug.WriteLine(e);
            }

            return RedirectToAction("Index", "User");
        }

        public ActionResult All(IEnumerable<UserModel> model) {
            var userContext = new UserDbContext();
            var users = userContext.Users.ToList();
            return View(users);
        }

        public ActionResult Add(string id) {
            var userContext = new UserDbContext();
            var userId = User.Identity.GetUserId();
            var currentUser = userContext.Users.FirstOrDefault(u => u.UserId == userId);

            var friendsContext = new FriendsDbContext();
            friendsContext.Friends.Add(new FriendsModel {
                UserId = userId,
                FriendId = id,
                Category = "Friend"
            });
            friendsContext.SaveChanges();

            return RedirectToAction("All", "User");
        }

        public ActionResult Search(string srchterm)
        {
            if (srchterm == null)
            {
                srchterm = "joahn löfven";
            }
            string[] nameArr = srchterm.Split(' ');
            var userContext = new UserDbContext();
            List<UserModel> validUserList = new List<UserModel>();
            foreach (var word in nameArr) {
                validUserList.AddRange(userContext.Users.Where(u => u.FirstName == word).ToList());
                validUserList.AddRange(userContext.Users.Where(u => u.LastName == word));
            }
            return View(validUserList);
        }
    }
}