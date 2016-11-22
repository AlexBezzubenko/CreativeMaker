namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade_delete_ratings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Ratings", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Ratings", "ApplicationUser_Id");
            AddForeignKey("dbo.Ratings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Ratings", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ratings", "ApplicationUser_Id");
            AddForeignKey("dbo.Ratings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
