using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class VisitorsModel {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public string VisitedId { get; set; }
        public DateTime Date { get; set; }
    }

    public class VisitorsDbContext : DbContext {
        public DbSet<VisitorsModel> Friends { get; set; }

        public VisitorsDbContext() : base("JustTheTip") { }
    }
}