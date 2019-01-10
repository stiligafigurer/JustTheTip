using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JustTheTip.Models {
    public class VisitorsModel {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string VisitedId { get; set; }
        public DateTime Date { get; set; }
    }

    public class VisitorsDbContext : DbContext {
        public DbSet<VisitorsModel> Visitors { get; set; }

        public VisitorsDbContext() : base("JustTheTip") { }
    }
}