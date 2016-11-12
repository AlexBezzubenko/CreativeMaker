namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_rate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCreatives",
                c => new
                    {
                        ApplicationUserId = c.Int(nullable: false),
                        CreativeId = c.Int(nullable: false),
                        Rating = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Creative_Id = c.Long(),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.CreativeId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Creatives", t => t.Creative_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Creative_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCreatives", "Creative_Id", "dbo.Creatives");
            DropForeignKey("dbo.UserCreatives", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserCreatives", new[] { "Creative_Id" });
            DropIndex("dbo.UserCreatives", new[] { "ApplicationUser_Id" });
            DropTable("dbo.UserCreatives");
        }
    }
}
