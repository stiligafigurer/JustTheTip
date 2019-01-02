using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int PostID { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Content { get; set; }
        public virtual User Poster { get; set; }
        public virtual User Recipient { get; set; }
        [DataType(DataType.DateTime)]
        public virtual DateTime PostDate { get; set; }
    }
}