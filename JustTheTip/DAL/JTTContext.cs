using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace JustTheTip.Models {
    public class JTTContext : DbContext {
        public JTTContext() : base("JTTContext") {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Prevents the table names from being pluralized (User instead of Users)
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}