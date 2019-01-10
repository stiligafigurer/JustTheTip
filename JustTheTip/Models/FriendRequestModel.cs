using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class FriendRequestModel {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public string FriendId { get; set; }

        // 0 == Not seen, 1 == seen
        public bool Seen { get; set; }
    }

    public class FriendRequestDbContext : DbContext {
        public DbSet<FriendRequestModel> FriendRequests { get; set; }

        public FriendRequestDbContext() : base("JustTheTip") { }
    }
}