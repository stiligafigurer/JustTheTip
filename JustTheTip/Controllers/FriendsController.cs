using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustTheTip.Controllers {
    [Authorize]
    public class FriendsController : Controller {
        public ActionResult Index() {
            var userId = User.Identity.GetUserId();
            var userContext = new UserDbContext();

            var friendsContext = new FriendsDbContext();
            var friends = friendsContext.Friends.Where(u => u.UserId == userId);
            var friendList = new List<FriendsViewModel>();

            foreach (var friend in friends) {
                var user = userContext.Users.FirstOrDefault(u => u.UserId == friend.FriendId);
                friendList.Add(new FriendsViewModel {
                    UserId = friend.FriendId,
                    ProfilePicUrl = user.ProfilePicUrl,
                    FullName = user.FirstName + ' ' + user.LastName,
                    BirthYear = user.BirthDate.Value.Year
                });
            }

            var requestsContext = new FriendRequestDbContext();
            var requests = requestsContext.FriendRequests.Where(u => u.FriendId == userId);
            var requestList = new List<FriendsViewModel>();

            foreach (var request in requests)
            {
                var user = userContext.Users.FirstOrDefault(u => u.UserId == request.UserId);
                requestList.Add(new FriendsViewModel
                {
                    UserId = request.UserId,
                    ProfilePicUrl = user.ProfilePicUrl,
                    FullName = user.FirstName + ' ' + user.LastName,
                    BirthYear = user.BirthDate.Value.Year
                });
            }

            var friendCollection = new FriendCollectionViewModel {
                Friends = friendList,
                Requests = requestList
            };

            return View(friendCollection);
        }

        public ActionResult Accept(string id) {
            var friendsContext = new FriendsDbContext();
            var requestsContext = new FriendRequestDbContext();
            var userId = User.Identity.GetUserId();
            var requestToRemove = requestsContext.FriendRequests.FirstOrDefault(u => u.FriendId == userId && u.UserId == id);

            // Check that the users aren't already friends
            var friends = friendsContext.Friends.Where(u => u.User.UserId == userId && u.Friend.UserId == id);
            if (friends.Count() == 0) {
                friendsContext.Friends.Add(new FriendsModel {
                    UserId = userId,
                    FriendId = id,
                    Category = "Friend"
                });
                // You're my friend == I'm your friend (allows users to pick different categories for each other)
                friendsContext.Friends.Add(new FriendsModel {
                    UserId = id,
                    FriendId = userId,
                    Category = "Friend"
                });

                friendsContext.SaveChanges();
            }
            requestsContext.FriendRequests.Remove(requestToRemove);
            requestsContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Ignore(string id) {
            var requestsContext = new FriendRequestDbContext();
            var userId = User.Identity.GetUserId();
            var requestToRemove = requestsContext.FriendRequests.FirstOrDefault(u => u.UserId == id && u.FriendId == userId);
            if (requestToRemove != null) {
                requestsContext.FriendRequests.Remove(requestToRemove);
                requestsContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id) {
            var friendsContext = new FriendsDbContext();
            var userId = User.Identity.GetUserId();
            var friendshipsToRemove = friendsContext.Friends.Where(u =>
                u.UserId == userId && u.FriendId == id ||
                u.UserId == id && u.FriendId == userId);
            foreach (var friendship in friendshipsToRemove) {
                friendsContext.Friends.Remove(friendship);
            }
            friendsContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult NewRequests() {
            var hasRequests = "false";
            var requestsContext = new FriendRequestDbContext();
            var userId = User.Identity.GetUserId();
            var unseenRequests = requestsContext.FriendRequests.Where(r => r.FriendId == userId && r.Seen == false).Count();

            if (unseenRequests > 0) {
                hasRequests = "true";
            }

            return Content(hasRequests);
        }
    }
}