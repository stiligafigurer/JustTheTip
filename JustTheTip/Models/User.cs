using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class User
    {
        public virtual int UserID { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Gender { get; set; }
        public virtual string SexualOrientation { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual string ProfilePicUrl { get; set; }
        public virtual string ZodiacSign { get; set; }
        public virtual string Country { get; set; }
        public virtual string District { get; set; }
        //ActiveUser 1 = true, 0 = false
        public virtual int ActiveUser { get; set; } 
    }
}