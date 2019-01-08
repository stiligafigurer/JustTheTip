using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class FriendRequestModel {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public virtual string FriendId { get; set; }

        public virtual bool Seen { get; set; }
    }

    public class FriendRequestDbContext : DbContext {
        public DbSet<FriendRequestModel> FriendRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            Database.SetInitializer<FriendRequestDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public FriendRequestDbContext() : base("JustTheTip") { }

        public DbSet<FriendsViewModel> FriendsViewModels { get; set; }
    }
}