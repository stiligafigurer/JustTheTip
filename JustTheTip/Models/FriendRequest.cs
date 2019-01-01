using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class FriendRequest {
        [ForeignKey("User")]
        public virtual User RequesterID { get; set; }
        [ForeignKey("User")]
        public virtual User RecipientID { get; set; }
        public virtual DateTime RequestDate { get; set; }
    }
}