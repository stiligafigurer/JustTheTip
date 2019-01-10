using JustTheTip.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JustTheTip.Controllers {
    [RoutePrefix("api/posts")]
    public class PostApiController : ApiController {
        [HttpGet]
        [Route("getposts")]
        public PostViewModel[] GetPosts(string id) { // api/posts/getall?id=XXX
            var posts = new PostDbContext().Posts.Where(p => p.RecipientId == id);
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

        [HttpGet]
        [Route("getuser")]
        public UserModel GetUser(string id) { // api/posts/getuser?id=XXX
            return new UserDbContext().Users.FirstOrDefault(u => u.UserId == id);
        }
    }
}
