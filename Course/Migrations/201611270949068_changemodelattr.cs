namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodelattr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Badges", "Name", c => c.String());
            AlterColumn("dbo.Creatives", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Headers", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Headers", "Name", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Creatives", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Badges", "Name", c => c.String(maxLength: 20));
        }
    }
}
