using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class PostModel {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        //[ForeignKey("Poster")]
        public string PosterId { get; set; }
        //public virtual UserModel Poster {get; set;}

        //[ForeignKey("Recipient")]
        public string RecipientId { get; set; }
        //public virtual UserModel Recipient { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }

    public class PostDbContext : DbContext {
        public DbSet<PostModel> Friends { get; set; }

        public PostDbContext() : base("JustTheTip") { }
    }
}