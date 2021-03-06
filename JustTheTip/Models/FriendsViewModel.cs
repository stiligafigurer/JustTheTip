﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JustTheTip.Models {
    public class FriendsViewModel {
        [Key]
        public string UserId { get; set; }
        [Display(Name = "Profile picture")]
        public string ProfilePicUrl { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Born")]
        public int BirthYear { get; set; }
        public string Category { get; set; }
    }

    public class FriendCollectionViewModel {
        public IEnumerable<FriendsViewModel> Friends { get; set; }
        public IEnumerable<FriendsViewModel> Requests { get; set; }
    }
}