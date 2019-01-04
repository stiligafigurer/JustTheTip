using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class UserModel
    {
        [Key]
        public virtual string UserId { get; set; }
        [Required, Display(Name = "First name")]
        public virtual string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        public virtual string LastName { get; set; }
        [Required]
        public virtual string Gender { get; set; }
        [Required, Display(Name = "Sexual orientation")]
        public virtual string SexualOrientation { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date of birth")]
        public virtual DateTime? BirthDate { get; set; }
        [DataType(DataType.ImageUrl), Display(Name = "Profile picture (url)")]
        public virtual string ProfilePicUrl { get; set; }
        [Display(Name = "Zodiac sign")]
        public virtual string ZodiacSign { get; set; }
        public virtual string Country { get; set; }
        //ActiveUser 1 = true, 0 = false
        public virtual int? ActiveUser { get; set; }
    }

    public class UserDbContext : DbContext {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<FriendsModel> Friends { get; set; }

        public UserDbContext() : base("JustTheTip") { }
    }
}