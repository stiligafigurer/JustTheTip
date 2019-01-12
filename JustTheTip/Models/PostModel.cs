using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class PostModel {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        public string PosterId { get; set; }
        public string RecipientId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }

    public class PostViewModel {
        [Key]
        public int PostId { get; set; }
        public string PosterId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public string ProfilePicUrl { get; set; }
        public string PosterName { get; set; }
    }

    public class PostExportModel {
        public string Recipient { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }

    public class PostDbContext : DbContext {
        public DbSet<PostModel> Posts { get; set; }

        public PostDbContext() : base("JustTheTip") { }
    }
}