using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual int PostID { get; set; }
        public virtual string Content { get; set; }
        public virtual User Poster { get; set; }
        public virtual User Recipient { get; set; }
        public virtual DateTime PostDate { get; set; }
    }
}