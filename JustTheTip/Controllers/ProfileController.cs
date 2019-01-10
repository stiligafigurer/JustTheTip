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
        public ActionResult Index(string ProfileId)
        {
            var model = new ProfileViewModel();
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                var userContext = new UserDbContext();
                var friendContext = new FriendsDbContext();
                var friendReqContext = new FriendRequestDbContext();
                var postContext = new PostDbContext();
                var interestContext = new InterestsDbContext();

                //Checks if the current user is the owner of the profile
                if (userId != ProfileId && ProfileId != null)
                {
                    userId = ProfileId;
                };
                var user = userContext.Users.FirstOrDefault(u => u.UserId == userId);
                List<InterestsModel> interestsList = interestContext.Friends.Where(u => u.UserId == userId).ToList();
                List<FriendsModel> friendList = friendContext.Friends.Where
                    (u => u.UserId == userId).ToList();
                List<PostModel> PostList = postContext.Friends.Where(u => u.RecipientId == userId).ToList();
                
                var UserPostList = new List<UserPostViewModel>();
                //Puts all posts to the user in a viewmodel with post- and user info
                foreach (var item in PostList)
                {
                    UserModel userInfo = userContext.Users.FirstOrDefault(u => u.UserId == item.PosterId);
                    var modelView = new UserPostViewModel
                    {
                        PostId = item.PostId,
                        PosterId = item.PosterId,
                        RecipientId = item.RecipientId,
                        Content = item.Content,
                        Date = item.Date,
                        ProfilePicUrl = userInfo.ProfilePicUrl,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                    };
                    UserPostList.Add(modelView);
                }
                var friendRequestList = friendReqContext.FriendRequests.ToList();
                //checks if any friendrequest between the users exists. False disables add friend
                foreach(var item in friendRequestList)
                {
                    if (item.FriendId == ProfileId && item.UserId == User.Identity.GetUserId())
                    {
                        model.HasPendingRequest = true;
                    }
                }
                //Checks if the users are already friends. False disables add friend.
                foreach(var item in friendList)
                {
                    if (item.UserId == ProfileId && item.FriendId == User.Identity.GetUserId())
                    {
                        model.IsFriend = true;
                    }
                }

                var UserDict = new Dictionary<UserModel, string>();
                //Puts all friends in a dictionary and pairs them with their appointed category
                foreach (var item in friendList)
                {
                    UserDict.Add(userContext.Users.FirstOrDefault(u => u.UserId == item.FriendId), item.Category);
                }
                model.UserId = user.UserId;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Gender = user.Gender;
                model.SexualOrientation = user.SexualOrientation;
                model.ZodiacSign = user.ZodiacSign;
                model.ProfilePicUrl = user.ProfilePicUrl;
                model.BirthDate = user.BirthDate;
                model.Country = user.Country;
                model.Friends = UserDict;
                model.Compatibility = CheckCompatibility(user.UserId);
                model.Posts = UserPostList;
                model.Interests = interestsList;

                return View(model);
              }

            return View("~/Views/Home/index.cshtml");
        }
        private int CheckCompatibility(string id) {
            var compatibility = 0;
            var userContext = new UserDbContext();
            var userId = User.Identity.GetUserId();
            if(id == userId)
            {
                return -1;
            }

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

        public ActionResult Add(string id)
        {
            var userContext = new UserDbContext();
            var friendsContext = new FriendsDbContext();
            var requestsContext = new FriendRequestDbContext();
            var userId = User.Identity.GetUserId();

            // Check that the sender hasn't already sent a request
            var sentRequest = requestsContext.FriendRequests.Where(u => u.UserId == userId && u.FriendId == id);
            if (sentRequest.Count() == 0)
            {
                // Check that the recipient hasn't already sent a request (redirect to requests page if they have)
                var receivedRequest = requestsContext.FriendRequests.Where(u => u.UserId == id && u.FriendId == userId);
                if (receivedRequest.Count() == 0)
                {
                    // Check that the users aren't already friends && that the user isn't trying to add themself
                    var friends = friendsContext.Friends.Where(u => u.User.UserId == userId && u.Friend.UserId == id);
                    if (friends.Count() == 0 && userId != id)
                    {
                        requestsContext.FriendRequests.Add(new FriendRequestModel
                        {
                            UserId = userId,
                            FriendId = id,
                            Seen = false
                        });
                        requestsContext.SaveChanges();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Friends");
                }
            }
            // TODO: Change to appropriate action once the method has been moved to ProfileController
            return (RedirectToAction("Index", "Profile", new { profileId = id }));
        }


        public ActionResult CompabilitySearch(string userId)
        {
            var userContext = new UserDbContext();
            var userDict = new Dictionary<UserModel, int>();
            List<UserModel> userList = new List<UserModel>();
            userList.AddRange(userContext.Users.Where(u => u.UserId != userId));
            foreach (var user in userList)
            {
                var score = CheckCompatibility(user.UserId);
                userDict.Add(user, score);
            }
            //Dictionary<UserModel, int> orderedUserDict = (Dictionary<UserModel, int>)userDict.OrderBy(x => x.Value);
            return View("~/Views/User/CompabilitySearch.cshtml", userDict);
        }
    }
}