using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SexualOrientation { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ZodiacSign { get; set; }
        public string ProfilePicUrl { get; set; }
        public List<UserModel> Friends { get; set; } 
    }
}