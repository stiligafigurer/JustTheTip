namespace JustTheTip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class walla2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserModels", "ProfilePicUrl", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "ZodiacSign", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "Country", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserModels", "Country", c => c.String());
            AlterColumn("dbo.UserModels", "ZodiacSign", c => c.String());
            AlterColumn("dbo.UserModels", "ProfilePicUrl", c => c.String());
        }
    }
}
