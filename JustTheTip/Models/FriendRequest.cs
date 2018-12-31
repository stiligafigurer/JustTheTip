using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class FriendRequest
    {
        public virtual User Requester { get; set; }
        public virtual User Recipient { get; set; }
        public virtual DateTime RequestDate { get; set; }
    }
}