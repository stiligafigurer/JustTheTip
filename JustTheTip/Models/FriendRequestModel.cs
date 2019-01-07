using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class FriendRequestModel {
        [Key, ForeignKey("User"), Column(Order = 0)]
        public string UserId { get; set; }
        public virtual UserModel User { get; set; }

        [Key, ForeignKey("Friend"), Column(Order = 1)]
        public virtual string FriendId { get; set; }
        public virtual UserModel Friend { get; set; }

        // 0 == Not seen, 1 == seen
        public bool Seen { get; set; }
    }

    public class FriendRequestContext : DbContext {
        public DbSet<FriendRequestModel> FriendRequests { get; set; }

        public FriendRequestContext() : base("JustTheTip") { }
    }
}