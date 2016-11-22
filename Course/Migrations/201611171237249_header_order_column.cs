namespace Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class header_order_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Headers", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Headers", "Order");
        }
    }
}
