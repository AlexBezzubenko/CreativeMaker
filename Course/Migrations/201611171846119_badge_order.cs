namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class badge_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Badges", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Badges", "Order");
        }
    }
}
