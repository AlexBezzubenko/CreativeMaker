namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class badgeamount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Badges", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Badges", "Amount");
        }
    }
}
