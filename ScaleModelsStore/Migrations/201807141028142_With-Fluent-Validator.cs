namespace ScaleModelsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithFluentValidator : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Orders", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Orders", "Phone", c => c.String(maxLength: 30));
            AlterColumn("dbo.Orders", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
