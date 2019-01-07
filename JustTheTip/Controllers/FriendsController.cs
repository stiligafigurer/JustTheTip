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
            var requestsContext = new FriendRequestDbContext();
            var userId = User.Identity.GetUserId();
            var requests = requestsContext.FriendRequests.Where(u => u.UserId == userId);

            var userContext = new UserDbContext();
            var requestList = new List<FriendsViewModel>();

            foreach (var request in requests) {
                var user = userContext.Users.FirstOrDefault(u => u.UserId == request.UserId);
                requestList.Add(new FriendsViewModel {
                    UserId = request.UserId,
                    ProfilePicUrl = user.ProfilePicUrl,
                    FullName = user.FirstName + ' ' + user.LastName,
                    BirthYear = user.BirthDate.Value.Year
                });
            }

            return View(requestList);
        }

        public ActionResult Accept(string id) {
            var friendsContext = new FriendsDbContext();
            var userId = User.Identity.GetUserId();

            friendsContext.Friends.Add(new FriendsModel {
                UserId = userId,
                FriendId = id,
                Category = "Friend"
            });

            try {
                friendsContext.SaveChanges();
                var requestsContext = new FriendRequestDbContext();
                var requestToRemove = requestsContext.FriendRequests.FirstOrDefault(u => u.UserId == userId && u.FriendId == id);
                requestsContext.FriendRequests.Remove(requestToRemove);
                requestsContext.SaveChanges();
            } catch {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id) {
            var requestsContext = new FriendRequestDbContext();
            var userId = User.Identity.GetUserId();
            var requestToRemove = requestsContext.FriendRequests.FirstOrDefault(u => u.UserId == userId && u.FriendId == id);
            requestsContext.FriendRequests.Remove(requestToRemove);
            requestsContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}