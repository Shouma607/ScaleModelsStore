namespace ScaleModelsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityEdited : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "PostalCode");
            DropColumn("dbo.Users", "Country");
            DropColumn("dbo.Users", "City");
            DropColumn("dbo.Users", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Address", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "City", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "Country", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "PostalCode", c => c.String(maxLength: 10));
        }
    }
}
