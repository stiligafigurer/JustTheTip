using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class FriendsModel {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        public virtual UserModel User { get; set; }

        [Key, Column(Order = 1)]
        public string FriendId { get; set; }
        public virtual UserModel Friend { get; set; }

        [Required, StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Category { get; set; }
    }

    public class FriendsExportModel {
        public string Name { get; set; }
    }

    public class FriendsDbContext : DbContext {
        public DbSet<FriendsModel> Friends { get; set; }

        public FriendsDbContext() : base("JustTheTip") { }
    }
}
