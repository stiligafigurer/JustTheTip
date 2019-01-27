﻿using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

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
                BirthDate = (DateTime)currentUser?.BirthDate,
                ProfilePic = currentUser?.ProfilePic,
                ZodiacSign = currentUser?.ZodiacSign,
                Country = currentUser?.Country,
                ActiveUser = currentUser?.ActiveUser
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ProfilePic")] UserModel model) {
            var userContext = new UserDbContext();
            var userId = User.Identity.GetUserId();
            var currentUser = userContext.Users.FirstOrDefault(u => u.UserId == userId);

            byte[] imgData = null;
            if (Request.Files.Count > 0) {
                HttpPostedFileBase imgFile = Request.Files["ProfilePic"];
                using (var binary = new BinaryReader(imgFile.InputStream)) {
                    imgData = binary.ReadBytes(imgFile.ContentLength);
                }
            }

            if (currentUser == null) {
                userContext.Users.Add(new UserModel {
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    SexualOrientation = model.SexualOrientation,
                    BirthDate = model.BirthDate.Value,
                    ProfilePic = imgData,
                    ZodiacSign = model.ZodiacSign,
                    Country = model.Country,
                    ActiveUser = currentUser.ActiveUser
                });
            } else {
                currentUser.UserId = userId;
                currentUser.FirstName = model.FirstName;
                currentUser.LastName = model.LastName;
                currentUser.Gender = model.Gender;
                currentUser.SexualOrientation = model.SexualOrientation;
                currentUser.BirthDate = model.BirthDate.Value;
                currentUser.ProfilePic = imgData;


                currentUser.ZodiacSign = model.ZodiacSign;
                currentUser.Country = model.Country;
                currentUser.ActiveUser = currentUser.ActiveUser;
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
 
        [HttpGet]
        public ActionResult Search(string srchterm)
        {
            //Splits the query into words and searches db for people with first- or last names matching the query
            string[] nameArr = srchterm.Split(' ');
            var userContext = new UserDbContext();
            List<UserModel> validUserList = new List<UserModel>();
            foreach (var word in nameArr) {
                validUserList.AddRange(userContext.Users.Where(u => u.FirstName == word && u.ActiveUser == 1));
                validUserList.AddRange(userContext.Users.Where(u => u.LastName == word & u.ActiveUser == 1));
            }
            var validUserListNoDup = new List<UserModel>();
            //Checks if there are any duplicate users in the result and removes them
            for(int i = 0; i < validUserList.Count; i++)
            {
                bool hasMatch = false;
                for (int e = i + 1; e < validUserList.Count; e++) {

                    if (validUserList[i].UserId == validUserList[e].UserId) {
                        hasMatch = true;
                    }
                }
                if (!hasMatch) {
                    validUserListNoDup.Add(validUserList[i]);
                }
            }
            return View(validUserListNoDup);
        }

        public ActionResult DeactivateAccount() {
            //Sets ActiveUser to 0 (0 = inactive, 1 = active)
            var id = User.Identity.GetUserId();
            var userContext = new UserDbContext();
            var currentUser = userContext.Users.FirstOrDefault(u => u.UserId == id);
            currentUser.ActiveUser = 0;
            userContext.SaveChanges();
            //Calls default logout action
            return RedirectToAction("LogOffNoToken", "Account");
        }

        public void ExportProfile() {
            var userId = User.Identity.GetUserId();

            var identityContext = new IdentityDbContext();
            identityContext.Configuration.ProxyCreationEnabled = false;
            var userContext = new UserDbContext();
            userContext.Configuration.ProxyCreationEnabled = false;
            var friendsContext = new FriendsDbContext();
            friendsContext.Configuration.ProxyCreationEnabled = false;
            var postsContext = new PostDbContext();
            postsContext.Configuration.ProxyCreationEnabled = false;

            var email = identityContext.Users.FirstOrDefault(u => u.Id == userId).Email;

            var posts = postsContext.Posts.Where(p => p.PosterId == userId).ToList();
            var postList = new List<PostExportModel>();
            foreach (var post in posts) {
                var recipient = userContext.Users.FirstOrDefault(u => u.UserId == post.RecipientId);
                postList.Add(new PostExportModel {
                    Recipient = recipient.FirstName + " " + recipient.LastName,
                    Content = post.Content,
                    Date = post.Date
                });
            }

            var friends = friendsContext.Friends.Where(f => f.UserId == userId).ToList();
            var friendsList = new List<FriendsExportModel>();
            foreach (var friend in friends) {
                var userFriend = userContext.Users.FirstOrDefault(u => u.UserId == friend.FriendId);
                friendsList.Add(new FriendsExportModel {
                    Name = userFriend.FirstName + " " + userFriend.LastName
                });
            }

            var user = userContext.Users.FirstOrDefault(u => u.UserId == userId);
            var userDetails = new UserExportModel {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                SexualOrientation = user.SexualOrientation,
                BirthDate = user.BirthDate,
                ProfilePic = user.ProfilePic,
                ZodiacSign = user.ZodiacSign,
                Country = user.Country
            };

            var userInfo = new AllUserData {
                User = userDetails,
                Friends = friendsList,
                Posts = postList
            };

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=jtt-profile.xml");
            Response.ContentType = "text/xml";

            var xs = new System.Xml.Serialization.XmlSerializer(userInfo.GetType());
            xs.Serialize(Response.OutputStream, userInfo);
        }
    }

    public class AllUserData {
        public UserExportModel User;
        public List<FriendsExportModel> Friends;
        public List<PostExportModel> Posts;
    }
}