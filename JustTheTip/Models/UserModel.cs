using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class UserModel {
        [Required, Key]
        public string UserId { get; set; }
        [Required, Display(Name = "First name")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} character long.", MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} character long.", MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required, Display(Name = "Sexual orientation")]
        public string SexualOrientation { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BirthDate { get; set; }
        [Required, DataType(DataType.ImageUrl), Display(Name = "Profile picture (URL)")]
        public string ProfilePicUrl { get; set; }
        [Required, Display(Name = "Zodiac sign")]
        public string ZodiacSign { get; set; }
        [Required]
        public string Country { get; set; }
        //ActiveUser 1 = true, 0 = false
        public int? ActiveUser { get; set; }
    }

    public class UserExportModel {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SexualOrientation { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ProfilePicUrl { get; set; }
        public string ZodiacSign { get; set; }
        public string Country { get; set; }
    }

    public class UserDbContext : DbContext {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<FriendsModel> Friends { get; set; }
        public DbSet<FriendRequestModel> FriendRequests { get; set; }

        public UserDbContext() : base("JustTheTip") { }
    }
}