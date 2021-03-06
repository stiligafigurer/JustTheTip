﻿using System.ComponentModel.DataAnnotations;
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

        public string Category { get; set; }
    }

    public class FriendsDbContext : DbContext {
        public DbSet<FriendsModel> Friends { get; set; }

        public FriendsDbContext() : base("JustTheTip") { }
    }
}
