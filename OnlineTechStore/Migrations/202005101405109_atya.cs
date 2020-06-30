namespace OnlineTechStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atya : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderDetails", "Stock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Stock", c => c.Int(nullable: false));
        }
    }
}
