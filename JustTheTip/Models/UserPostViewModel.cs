using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class UserPostViewModel
    {
        public int PostId { get; set; }
        public string PosterId { get; set; }
        public string RecipientId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string ProfilePicUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}