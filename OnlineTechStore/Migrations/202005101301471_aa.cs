namespace OnlineTechStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropColumn("dbo.Orders", "CustomerId");
            RenameColumn(table: "dbo.Orders", name: "Customer_Id", newName: "CustomerId");
            AlterColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "CustomerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            AlterColumn("dbo.Orders", "CustomerId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "CustomerId", newName: "Customer_Id");
            AddColumn("dbo.Orders", "CustomerId", c => c.Int());
            CreateIndex("dbo.Orders", "Customer_Id");
        }
    }
}
