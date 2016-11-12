namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Bages", newName: "Badges");
            RenameTable(name: "dbo.UserCreatives", newName: "Ratings");
            AddColumn("dbo.Ratings", "Value", c => c.Double(nullable: false));
            DropColumn("dbo.Ratings", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Rating", c => c.Double(nullable: false));
            DropColumn("dbo.Ratings", "Value");
            RenameTable(name: "dbo.Ratings", newName: "UserCreatives");
            RenameTable(name: "dbo.Badges", newName: "Bages");
        }
    }
}
