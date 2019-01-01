using System;
using System.ComponentModel.DataAnnotations;

namespace JustTheTip.Models {
    public class FriendRequest
    {
        [Key]
        public virtual User Requester { get; set; }
        [Key]
        public virtual User Recipient { get; set; }
        public virtual DateTime RequestDate { get; set; }
    }
}