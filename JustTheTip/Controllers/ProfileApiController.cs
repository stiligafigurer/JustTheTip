using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JustTheTip.Controllers {
    [RoutePrefix("api/profile")]
    public class ProfileApiController : ApiController {
        [HttpGet]
        [Route("get-posts")]
        public PostViewModel[] Get(string id) { // api/profile/get-posts?id=XXX
            var posts = new PostDbContext().Posts.Where(p => p.RecipientId == id).OrderByDescending(p => p.Date);
            var postList = new List<PostViewModel>();
            foreach (var post in posts) {
                var user = new UserDbContext().Users.FirstOrDefault(u => u.UserId == post.PosterId);
                postList.Add(new PostViewModel {
                    PostId = post.PostId,
                    PosterId = post.PosterId,
                    Content = post.Content,
                    Date = post.Date,
                    ProfilePicUrl = user.ProfilePicUrl,
                    PosterName = user.FirstName + " " + user.LastName
                });
            }
            return postList.ToArray();
        }

        [HttpPost]
        [Route("submit-post")]
        public IHttpActionResult Submit([FromBody] PostModel post) { // api/profile/submit-post?recipient=XXX?content=YYY
            var userId = User.Identity.GetUserId();
            // HTML-Encode the post content to prevent any funny business
            var content = HttpUtility.HtmlEncode(post.Content);
            try {
                var postContext = new PostDbContext();
                postContext.Posts.Add(new PostModel {
                    PosterId = userId,
                    RecipientId = post.RecipientId,
                    Content = content,
                    Date = System.DateTime.Now
                });
                postContext.SaveChanges();
                return base.Ok();
            } catch {
                return base.BadRequest();
            }
        }

        [HttpPost]
        [Route("remove-post")]
        public IHttpActionResult Remove(int id) { // api/profile/remove-post?id=XXX
            var userId = User.Identity.GetUserId();
            var postContext = new PostDbContext();
            var postToRemove = postContext.Posts.FirstOrDefault(p => p.PostId == id);

            // Make sure the request is coming from the correct user
            if (postToRemove.RecipientId == userId) {
                try {
                    postContext.Posts.Remove(postToRemove);
                    postContext.SaveChanges();
                    return base.Ok();
                } catch {
                    return base.BadRequest();
                }
            } else {
                return base.BadRequest();
            }
        }

        [HttpPost]
        [Route("visit")]
        public IHttpActionResult Visit([FromBody] string id) { // api/profile/visit?request=XXX
            var userId = User.Identity.GetUserId();
            var visitorsContext = new VisitorsDbContext();

            // Only log visits to other people's profiles
            if (id != null && id != userId) {

                var previousVisits = visitorsContext.Visitors.Where(v => v.UserId == userId && v.VisitedId == id);
                foreach (var visit in previousVisits) {
                    visitorsContext.Visitors.Remove(visit);
                }

                visitorsContext.Visitors.Add(new VisitorsModel {
                    UserId = userId,
                    VisitedId = id,
                    Date = System.DateTime.Now
                });

                try {
                    visitorsContext.SaveChanges();
                } catch {
                    return base.BadRequest();
                }
            }
            return base.Ok();
        }
    }
}
