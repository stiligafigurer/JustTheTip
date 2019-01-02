using JustTheTip.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace JustTheTip.DAL {
    public class JTTContext : DbContext {
        public JTTContext() : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FriendCategory> FriendCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            // Prevents the table names from being pluralized (User instead of Users)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<User>().HasMany(m => m.Friends).WithMany().Map(m => {
            //    m.MapLeftKey("User");
            //    m.MapRightKey("Friend");
            //    m.ToTable("Friends");
            //});

            //modelBuilder.Entity<User>().HasMany(m => m.FriendRequests).WithMany().Map(m => {
            //    m.MapLeftKey("User");
            //    m.MapRightKey("Friend");
            //    m.ToTable("FriendRequests");
            //});
        }
    }
}