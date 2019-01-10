using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class InterestsModel {
        [Key]
        public string UserId { get; set; }
        public string Interest { get; set; }
    }

    public class InterestsDbContext : DbContext {
        public DbSet<InterestsModel> Friends { get; set; }

        public InterestsDbContext() : base("JustTheTip") { }
    }
}