namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_rate2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserCreatives");
            AddColumn("dbo.UserCreatives", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserCreatives", "Id");
            DropColumn("dbo.UserCreatives", "ApplicationUserId");
            DropColumn("dbo.UserCreatives", "CreativeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserCreatives", "CreativeId", c => c.Int(nullable: false));
            AddColumn("dbo.UserCreatives", "ApplicationUserId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.UserCreatives");
            DropColumn("dbo.UserCreatives", "Id");
            AddPrimaryKey("dbo.UserCreatives", new[] { "ApplicationUserId", "CreativeId" });
        }
    }
}
