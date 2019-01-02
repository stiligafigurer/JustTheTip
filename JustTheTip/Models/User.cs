using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int UserID { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public virtual string Password { get; set; }
        [Required]
        public virtual string FirstName { get; set; }
        [Required]
        public virtual string LastName { get; set; }
        [Required]
        public virtual string Gender { get; set; }
        public virtual string SexualOrientation { get; set; }
        public virtual DateTime BirthDate { get; set; }
        [DataType(DataType.ImageUrl)]
        public virtual string ProfilePicUrl { get; set; }
        public virtual string ZodiacSign { get; set; }
        public virtual string Country { get; set; }
        public virtual string District { get; set; }
        //ActiveUser 1 = true, 0 = false
        public virtual int ActiveUser { get; set; }

        public virtual ICollection<User> Friends { get; set; }
        public virtual ICollection<User> FriendRequests { get; set; }
        public virtual ICollection<FriendCategory> FriendCategories { get; set; }
    }
}