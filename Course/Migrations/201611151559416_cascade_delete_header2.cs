namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade_delete_header2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Headers", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Headers", new[] { "Creative_Id" });
            AlterColumn("dbo.Headers", "Creative_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Headers", "Creative_Id");
            AddForeignKey("dbo.Headers", "Creative_Id", "dbo.Creatives", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Headers", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Headers", new[] { "Creative_Id" });
            AlterColumn("dbo.Headers", "Creative_Id", c => c.Long());
            CreateIndex("dbo.Headers", "Creative_Id");
            AddForeignKey("dbo.Headers", "Creative_Id", "dbo.Creatives", "Id");
        }
    }
}
