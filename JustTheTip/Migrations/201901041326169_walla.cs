namespace JustTheTip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class walla : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendsModels",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FriendId = c.String(nullable: false, maxLength: 128),
                        Category = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.FriendId })
                .ForeignKey("dbo.UserModels", t => t.FriendId, cascadeDelete: false)
                .ForeignKey("dbo.UserModels", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        SexualOrientation = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        ProfilePicUrl = c.String(),
                        ZodiacSign = c.String(),
                        Country = c.String(),
                        ActiveUser = c.Int(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendsModels", "UserId", "dbo.UserModels");
            DropForeignKey("dbo.FriendsModels", "FriendId", "dbo.UserModels");
            DropIndex("dbo.FriendsModels", new[] { "FriendId" });
            DropIndex("dbo.FriendsModels", new[] { "UserId" });
            DropTable("dbo.UserModels");
            DropTable("dbo.FriendsModels");
        }
    }
}
