using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class Post
    {
        public virtual int PostID { get; set; }
        public virtual string Content { get; set; }
        public virtual User Poster { get; set; }
        public virtual User Recipient { get; set; }
        public virtual DateTime PostDate { get; set; }
    }
}