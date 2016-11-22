namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade_delete_creatives : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Creatives", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Creatives", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Creatives", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Creatives", "ApplicationUser_Id");
            AddForeignKey("dbo.Creatives", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Creatives", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Creatives", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Creatives", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Creatives", "ApplicationUser_Id");
            AddForeignKey("dbo.Creatives", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
