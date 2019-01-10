using JustTheTip.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace JustTheTip.Controllers {
    [RoutePrefix("api/posts")]
    public class PostApiController : ApiController {
        [HttpGet]
        [Route("getposts")]
        public PostViewModel[] Get(string id) { // api/posts/get?id=XXX
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
        [Route("submit")]
        public IHttpActionResult Submit([FromBody] PostModel post) { // api/posts/submit?recipient=XXX?content=YYY
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
        [Route("remove")]
        public IHttpActionResult Remove(int id) { // api/posts/remove?id=XXX
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
    }
}
