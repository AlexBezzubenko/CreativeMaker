namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Creative_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creatives", t => t.Creative_Id)
                .Index(t => t.Creative_Id);
            
            CreateTable(
                "dbo.Creatives",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Views = c.Long(nullable: false),
                        Rating = c.Double(nullable: false),
                        RatingsAmount = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastEditTime = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Headers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Text = c.String(),
                        Creative_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creatives", t => t.Creative_Id)
                .Index(t => t.Creative_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Header_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Headers", t => t.Header_Id)
                .Index(t => t.Header_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Headers", "Creative_Id", "dbo.Creatives");
            DropForeignKey("dbo.Tags", "Header_Id", "dbo.Headers");
            DropForeignKey("dbo.Bages", "Creative_Id", "dbo.Creatives");
            DropForeignKey("dbo.Creatives", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tags", new[] { "Header_Id" });
            DropIndex("dbo.Headers", new[] { "Creative_Id" });
            DropIndex("dbo.Creatives", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Bages", new[] { "Creative_Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.Headers");
            DropTable("dbo.Creatives");
            DropTable("dbo.Bages");
        }
    }
}
