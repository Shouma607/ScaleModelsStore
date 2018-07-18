namespace ScaleModelsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithIdentity2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PostalCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Users", "Country", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "City", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "Address", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "City");
            DropColumn("dbo.Users", "Country");
            DropColumn("dbo.Users", "PostalCode");
        }
    }
}
