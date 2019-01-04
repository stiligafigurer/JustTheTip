using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JustTheTip.Models {
    public class FriendsModel {

        [Key]
        public virtual string UserId { get; set; }
        [Key]
        public virtual string FriendId { get; set; }
        public virtual string Category { get; set; }

        public class ProfileDbContext : DbContext {
            public DbSet<FriendsModel> Friends { get; set; }

            public ProfileDbContext() : base("JustTheTip") { }
        }
    }
}